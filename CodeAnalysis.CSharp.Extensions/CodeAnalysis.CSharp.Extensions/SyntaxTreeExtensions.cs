using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Microsoft.CodeAnalysis.CSharp.Extensions
{
    public static class SyntaxTreeExtensions
    {
        #region WithModifiers

        public static FieldDeclarationSyntax WithModifiers(this FieldDeclarationSyntax fieldDeclarationSyntax, params SyntaxKind[] modifiers)
        {
            var tokenList = new SyntaxTokenList();
            foreach (var modifier in modifiers)
            {
                tokenList = tokenList.Add(SyntaxFactory.Token(modifier));
            }
            return fieldDeclarationSyntax.WithModifiers(tokenList);
        }

        public static PropertyDeclarationSyntax WithModifiers(this PropertyDeclarationSyntax propertyDeclarationSyntax, params SyntaxKind[] modifiers)
        {
            var tokenList = new SyntaxTokenList();
            foreach (var modifier in modifiers)
            {
                tokenList = tokenList.Add(SyntaxFactory.Token(modifier));
            }
            return propertyDeclarationSyntax.WithModifiers(tokenList);
        }

        public static ClassDeclarationSyntax WithModifiers(this ClassDeclarationSyntax classDeclarationSyntax, params SyntaxKind[] modifiers)
        {
            var tokenList = new SyntaxTokenList();
            foreach (var modifier in modifiers)
            {
                tokenList = tokenList.Add(SyntaxFactory.Token(modifier));
            }
            return classDeclarationSyntax.WithModifiers(tokenList);
        }

        public static MethodDeclarationSyntax WithModifiers(this MethodDeclarationSyntax methodDeclarationSyntax, params SyntaxKind[] modifiers)
        {
            var tokenList = new SyntaxTokenList();
            foreach (var modifier in modifiers)
            {
                tokenList = tokenList.Add(SyntaxFactory.Token(modifier));
            }
            return methodDeclarationSyntax.WithModifiers(tokenList);
        }

        public static EnumDeclarationSyntax WithModifiers(this EnumDeclarationSyntax enumDeclarationSyntax, params SyntaxKind[] modifiers)
        {
            var tokenList = new SyntaxTokenList();
            foreach (var modifier in modifiers)
            {
                tokenList = tokenList.Add(SyntaxFactory.Token(modifier));
            }
            return enumDeclarationSyntax.WithModifiers(tokenList);
        }

        public static InterfaceDeclarationSyntax WithModifiers(this InterfaceDeclarationSyntax interfaceDeclarationSyntax, params SyntaxKind[] modifiers)
        {
            var tokenList = new SyntaxTokenList();
            foreach (var modifier in modifiers)
            {
                tokenList = tokenList.Add(SyntaxFactory.Token(modifier));
            }
            return interfaceDeclarationSyntax.WithModifiers(tokenList);
        }

        #endregion

        public static ClassDeclarationSyntax WithBaseList(this ClassDeclarationSyntax classDeclarationSyntax, params string[] baseList)
        {
            var baseListSyntax = SyntaxFactory.BaseList();

            foreach (var baseItem in baseList)
            {
                var baseType = SyntaxFactory.SimpleBaseType(SyntaxFactory.ParseTypeName(baseItem));

                baseListSyntax = baseListSyntax.AddTypes(baseType);
            }
            return classDeclarationSyntax.WithBaseList(baseListSyntax);
        }

        public static NamespaceDeclarationSyntax AddUsings(this NamespaceDeclarationSyntax namespaceDeclarationSyntax, params string[] usings)
        {
            foreach (var usingName in usings)
            {
                var name = SyntaxFactory.ParseName(usingName);
                var usingSyntax = SyntaxFactory.UsingDirective(name);
                namespaceDeclarationSyntax = namespaceDeclarationSyntax.AddUsings(usingSyntax);
            }
            return namespaceDeclarationSyntax;
        }


        public static AttributeSyntax AddArgument(this AttributeSyntax attributeSyntax, string name, object value)
        {
            return AddFormattedArgument(attributeSyntax, name, value, "{0} = {1}");
        }

        public static AttributeSyntax AddQuotedArgument(this AttributeSyntax attributeSyntax, string name, object value)
        {
            return AddFormattedArgument(attributeSyntax, name, value, "{0} = \"{1}\"");
        }

        public static AttributeSyntax AddArgument(this AttributeSyntax attributeSyntax, object value)
        {
            var expression = SyntaxFactory.ParseExpression(value.ToString());
            var argument = SyntaxFactory.AttributeArgument(expression);
            return attributeSyntax.AddArgumentListArguments(argument);
        }

        public static AttributeSyntax AddQuotedArgument(this AttributeSyntax attributeSyntax, object value)
        {
            var expression = SyntaxFactory.ParseExpression(string.Format("\"{0}\"", value));
            var argument = SyntaxFactory.AttributeArgument(expression);
            return attributeSyntax.AddArgumentListArguments(argument);
        }

        public static AttributeSyntax AddFormattedArgument(this AttributeSyntax attributeSyntax, string name, object value, string format)
        {
            ExpressionSyntax expression;
            if (value == null)
            {
                expression = SyntaxFactory.ParseExpression(string.Format("{0} = null", name));
            }
            else
            {
                expression = SyntaxFactory.ParseExpression(string.Format(format, name, value));
            }
            var argument = SyntaxFactory.AttributeArgument(expression);
            return attributeSyntax.AddArgumentListArguments(argument);
        }

        public static ClassDeclarationSyntax AddAttribute(this ClassDeclarationSyntax classDeclarationSyntax, AttributeSyntax attributeSyntax)
        {
            var attributeList = SyntaxFactory.AttributeList().AddAttributes(attributeSyntax);
            return classDeclarationSyntax.AddAttributeLists(attributeList);
        }

        public static PropertyDeclarationSyntax AddAttribute(this PropertyDeclarationSyntax propertyDeclarationSyntax, AttributeSyntax attributeSyntax)
        {
            var attributeList = SyntaxFactory.AttributeList().AddAttributes(attributeSyntax);
            return propertyDeclarationSyntax.AddAttributeLists(attributeList);
        }

        public static FieldDeclarationSyntax AddAttribute(this FieldDeclarationSyntax fieldDeclarationSyntax, AttributeSyntax attributeSyntax)
        {
            var attributeList = SyntaxFactory.AttributeList().AddAttributes(attributeSyntax);
            return fieldDeclarationSyntax.AddAttributeLists(attributeList);
        }

        public static EnumMemberDeclarationSyntax AddAttribute(this EnumMemberDeclarationSyntax enumMemberDeclarationSyntax, AttributeSyntax attributeSyntax)
        {
            var attributeList = SyntaxFactory.AttributeList().AddAttributes(attributeSyntax);
            return enumMemberDeclarationSyntax.AddAttributeLists(attributeList);
        }

        public static NamespaceDeclarationSyntax NamespaceDeclaration(string name)
        {
            return SyntaxFactory.NamespaceDeclaration(SyntaxFactory.ParseName(name));
        }

        public static AttributeSyntax Attribute(string name)
        {
            return SyntaxFactory.Attribute(SyntaxFactory.ParseName(name));
        }

        public static FieldDeclarationSyntax FieldDeclaration(TypeSyntax typeSyntax, string name)
        {
            var variable = SyntaxFactory.VariableDeclarator(name);
            var variableDecl = SyntaxFactory.VariableDeclaration(typeSyntax)
                .WithVariables(SyntaxFactory.SeparatedList(new[] { variable }));
            return SyntaxFactory.FieldDeclaration(variableDecl);
        }
    }
}
