using System.Collections.Generic;
using System.Linq;

namespace Microsoft.CodeAnalysis.CSharp.Extensions
{
    public static class SemanticTreeExtensions
    {
        #region ITypeSymbol.Get...

        public static IEnumerable<IFieldSymbol> GetFields(this ITypeSymbol typeSymbol)
        {
            return typeSymbol.GetMembers().Where(m => m.Kind == SymbolKind.Field).Cast<IFieldSymbol>();
        }

        public static IEnumerable<IPropertySymbol> GetProperties(this ITypeSymbol typeSymbol)
        {
            return typeSymbol.GetMembers().Where(m => m.Kind == SymbolKind.Property).Cast<IPropertySymbol>();
        }

        public static IEnumerable<IMethodSymbol> GetMethods(this ITypeSymbol typeSymbol)
        {
            return typeSymbol.GetMembers().Where(m => m.Kind == SymbolKind.Method).Cast<IMethodSymbol>();
        }

        public static IEnumerable<IEventSymbol> GetEvents(this ITypeSymbol typeSymbol)
        {
            return typeSymbol.GetMembers().Where(m => m.Kind == SymbolKind.Event).Cast<IEventSymbol>();
        }
        #endregion

        public static AttributeData GetAttribute(this ISymbol symbol, string attribute)
        {
            return symbol.GetAttributes().FirstOrDefault(attr => attr.AttributeClass.Name == attribute);
        }

        public static IEnumerable<AttributeData> GetAttributes(this ISymbol symbol, string attribute)
        {
            return symbol.GetAttributes().Where(attr => attr.AttributeClass.Name == attribute);
        }

        public static TypedConstant? GetNamedArgument(this AttributeData attributeData, string argumentName)
        {
            var argument = attributeData.NamedArguments.FirstOrDefault(item => item.Key == argumentName);

            if (argument.Equals(default(KeyValuePair<string, TypedConstant>))) return null;

            return argument.Value;
        }

        public static T GetValueOrDefault<T>(this TypedConstant? typedConstant)
        {
            if (typedConstant == null) return default(T);

            return (T)typedConstant.Value.Value;
        }

        private static readonly HashSet<string> PrimitiveTypeNames = new HashSet<string>
			{
				"byte", "sbyte", "short", "ushort", "int", "uint", "long", "ulong", "float", "double", "decimal", "char", "string", "bool", "object"
			};

        public static bool IsPrimitive(this ITypeSymbol typeSymbol)
        {
            return PrimitiveTypeNames.Contains(typeSymbol.ToString());
        }
    }
}
