using System;
using System.Reflection;
using System.Reflection.Emit;
using VivoPagamentoJudiciais.Model.Entities;
using VivoPagamentoJudiciais.Model.Enum;


namespace VivoPagamentoJudiciais.Utilities.DynamicClass
{
    public class ConstructClass
    {

        private static ConvertType convertType = new ConvertType();
        private static ETipoArquivo typeFile;

        public static dynamic CreateNewObject(GeracaoArquivo @class)
        {
            typeFile = (ETipoArquivo)@class.IdTipoArquivo;

            var myType = CompileResultType(@class);
            var myObject = (dynamic)Activator.CreateInstance(myType);

            return myObject;
        }

        public static Type CompileResultType(GeracaoArquivo @class)
        {
            TypeBuilder tb = GetTypeBuilder(@class.Descricao);
            ConstructorBuilder constructor = tb.DefineDefaultConstructor(MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.RTSpecialName);

            foreach (GeracaoArquivoClasse propriedade in @class.GeracaoArquivoClasse)
            {
                CreateProperty(tb, propriedade);

            }

            Type objectType = tb.CreateType();

            return objectType;
        }

        private static TypeBuilder GetTypeBuilder(string className)
        {
            var typeSignature = className;
            var an = new AssemblyName(typeSignature);
            AssemblyBuilder assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(an, AssemblyBuilderAccess.Run);
            ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule("MainModule");
            TypeBuilder tb = moduleBuilder.DefineType(typeSignature,
                    TypeAttributes.Public |
                    TypeAttributes.Class |
                    TypeAttributes.AutoClass |
                    TypeAttributes.AnsiClass |
                    TypeAttributes.BeforeFieldInit |
                    TypeAttributes.AutoLayout);
            return tb;
        }

        private static void CreateProperty(TypeBuilder tb, GeracaoArquivoClasse propriedade)
        {
            FieldBuilder fieldBuilder = tb.DefineField($"<{propriedade.NomePropriedade}>k__BackingField", convertType.GetType(propriedade.TipoPropriedade), FieldAttributes.Private | FieldAttributes.HasDefault);

            PropertyBuilder propertyBuilder = tb.DefineProperty(propriedade.NomePropriedade, PropertyAttributes.HasDefault, convertType.GetType(propriedade.TipoPropriedade), null);
            MethodBuilder getPropMthdBldr = tb.DefineMethod("get_" + propriedade.NomePropriedade, MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.HideBySig, convertType.GetType(propriedade.TipoPropriedade), Type.EmptyTypes);
            ILGenerator getIl = getPropMthdBldr.GetILGenerator();

            getIl.Emit(OpCodes.Ldarg_0);
            getIl.Emit(OpCodes.Ldfld, fieldBuilder);
            getIl.Emit(OpCodes.Ret);

            MethodBuilder setPropMthdBldr =
                tb.DefineMethod("set_" + propriedade.NomePropriedade,
                  MethodAttributes.Public |
                  MethodAttributes.SpecialName |
                  MethodAttributes.HideBySig,
                  null, new[] { convertType.GetType(propriedade.TipoPropriedade) });


            ILGenerator setIl = setPropMthdBldr.GetILGenerator();
            Label modifyProperty = setIl.DefineLabel();
            Label exitSet = setIl.DefineLabel();

            setIl.MarkLabel(modifyProperty);
            setIl.Emit(OpCodes.Ldarg_0);
            setIl.Emit(OpCodes.Ldarg_1);
            setIl.Emit(OpCodes.Stfld, fieldBuilder);

            setIl.Emit(OpCodes.Nop);
            setIl.MarkLabel(exitSet);
            setIl.Emit(OpCodes.Ret);

            propertyBuilder.SetGetMethod(getPropMthdBldr);
            propertyBuilder.SetSetMethod(setPropMthdBldr);

            if (propriedade.CustomAttribute != string.Empty && propriedade.CustomAttribute != null)
            {
                Type caType = convertType.GetType(propriedade.CustomAttribute);
                CustomAttributeBuilder caBuilder;

                if (typeFile == ETipoArquivo.CSV)
                {
                    Type[] types = new Type[2];
                    types[0] = typeof(int);
                    types[1] = typeof(int);

                    caBuilder = new CustomAttributeBuilder(
                       caType.GetConstructor(types), new object[] { propriedade.PropridadesCsv.Index, -1 });

                    propertyBuilder.SetCustomAttribute(caBuilder);

                    return;
                }

                caBuilder = new CustomAttributeBuilder(
                   caType.GetConstructor(Type.EmptyTypes), new object[] { });

                propertyBuilder.SetCustomAttribute(caBuilder);
            }
        }
    }
}
