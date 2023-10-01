using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

class Program
{
    static void Main()
    {
        // See https://aka.ms/new-console-template for more information
        Console.WriteLine("Hello, World!");
        // Specify the directory to be manipulated.
        string path = "./input";

        // Get the files in the directory and print out the ones with .cs extension.
        foreach (string file in Directory.GetFiles(path, "*.cs"))
        {
            string fileName = Path.GetFileName(file);
            Console.WriteLine($"File Name: {fileName}");

            // Read and print the content of each .cs file
            string contents = File.ReadAllText(file);
            Console.WriteLine($"Contents: {contents}");

            SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(contents);

            var root = syntaxTree.GetRoot();
            var walker = new MySyntaxWalker();
            walker.Visit(root);

            Console.WriteLine(walker.Output);
        }
        Console.ReadLine();
    }
}

class MySyntaxWalker: CSharpSyntaxWalker
{
    public string Output { get; private set; } = "";

    public override void Visit(SyntaxNode node)
    {
        // Check if the node is a variable declaration
        if (node is VariableDeclarationSyntax variableDeclaration)
        {
            // Access the variable name
            string variableName = variableDeclaration.Variables.FirstOrDefault()?.Identifier.Text;

            // Output the variable name
            Output += new string(' ', Depth * 2) + "- Variable Declaration: " + variableName + Environment.NewLine;
        }
        else if (node is IdentifierNameSyntax identifierName)
        {
            string name = identifierName.Identifier.Text;
            Output += new string(' ', Depth * 2) + "- Identifier Name: " + name + Environment.NewLine;
        }
        else if (node is ClassDeclarationSyntax classDeclaration)
        {
            string className = classDeclaration.Identifier.Text;
            Output += new string(' ', Depth * 2) + "- Class Declaration: " + className + Environment.NewLine;
        }
        else if (node is MethodDeclarationSyntax methodDeclaration)
        {
            string methodName = methodDeclaration.Identifier.Text;
            Output += new string(' ', Depth * 2) + "- Method Declaration: " + methodName + Environment.NewLine;
        }
        else if (node is PredefinedTypeSyntax predefinedType)
        {
            string typeName = predefinedType.Keyword.Text;
            Output += new string(' ', Depth * 2) + "- Predefined Type: " + typeName + Environment.NewLine;
        }
        else if (node is VariableDeclaratorSyntax variableDeclarator)
        {
            string variableDeclaratorName = variableDeclarator.Identifier.Text;
            Output += new string(' ', Depth * 2) + "- Variable Declarator: " + variableDeclaratorName + Environment.NewLine;
        }
        else if (node is InvocationExpressionSyntax invocationExpression)
        {
            string methodName = invocationExpression.Expression.ToString(); // メソッド名を取得
            Output += new string(' ', Depth * 2) + "- Invocation Expression: " + methodName + Environment.NewLine;
        }
        else if (node is ExpressionStatementSyntax expressionStatement)
        {
            string expressionText = expressionStatement.Expression.ToString(); // 式のテキストを取得
            Output += new string(' ', Depth * 2) + "- Expression Statement: " + expressionText + Environment.NewLine;
        }
        else if (node is MemberAccessExpressionSyntax memberAccessExpression)
        {
            string memberName = memberAccessExpression.Name.Identifier.Text; // メンバー名を取得
            Output += new string(' ', Depth * 2) + "- Member Access Expression: " + memberName + Environment.NewLine;
        }
        // Add other specific checks for different node types and properties here
        else if (node is ReturnStatementSyntax returnStatement)
        {
            // ReturnStatementSyntaxの処理を追加
            Output += new string(' ', Depth * 2) + "- Return Statement"+ Environment.NewLine;
        }
        else if (node is LocalDeclarationStatementSyntax localDeclarationStatement)
        {
            // LocalDeclarationStatementSyntaxの処理を追加
            string localdeclarationStatement = localDeclarationStatement.ToString();
            Output += new string(' ', Depth * 2) + "- Local Declaration Statement: " + localdeclarationStatement + Environment.NewLine;
        }
        else if (node is BlockSyntax blockSyntax)
        {
            // BlockSyntaxの処理を追加
            Output += new string(' ', Depth * 2) + "- Block" + Environment.NewLine;
        }
        else if (node is CatchDeclarationSyntax catchDeclaration)
        {
            // CatchDeclarationSyntaxの処理を追加
            Output += new string(' ', Depth * 2) + "- Catch Declaration: " + catchDeclaration.Identifier.Text + Environment.NewLine;
        }
        else if (node is CatchClauseSyntax catchClause)
        {
            // CatchClauseSyntaxの処理を追加
            Output += new string(' ', Depth * 2) + "- Catch Clause" + Environment.NewLine;
        }
        else if (node is UsingDirectiveSyntax UsingDirective)
        {
            // CatchClauseSyntaxの処理を追加
            string usingdirective = UsingDirective.ToString();
            Output += new string(' ', Depth * 2) + "- UsingDirective: " + usingdirective + Environment.NewLine;
        }
        else if (node is ParameterListSyntax ParameterList)
        {
            // CatchClauseSyntaxの処理を追加
            string parameterlist = ParameterList.ToString();
            Output += new string(' ', Depth * 2) + "- ParameterList: " + parameterlist + Environment.NewLine;
        }
        else if (node is ArgumentListSyntax ArgumentList)
        {
            // CatchClauseSyntaxの処理を追加
            string argumentlist = ArgumentList.ToString();
            Output += new string(' ', Depth * 2) + "- ArgumentList: " + argumentlist + Environment.NewLine;
        }
        else if (node is ArgumentSyntax Argument)
        {
            // CatchClauseSyntaxの処理を追加
            string argument = Argument.ToString();
            Output += new string(' ', Depth * 2) + "- Argument: " + argument + Environment.NewLine;
        }
        else
        {
            string others = node.ToString();
            Console.WriteLine(others);
            Output += new string(' ', Depth * 2) + "- " + node.Kind() + Environment.NewLine;
        }

        Depth++;
        base.Visit(node);
        Depth--;
    }
    private int Depth { get; set; } = 0;
}
