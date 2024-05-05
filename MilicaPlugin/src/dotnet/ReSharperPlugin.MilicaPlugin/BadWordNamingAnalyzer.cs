using JetBrains.ReSharper.Feature.Services.Daemon;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Tree;

namespace ReSharperPlugin.MilicaPlugin.Highlighter;


[ElementProblemAnalyzer(typeof(IComment), HighlightingTypes =
    new[] {typeof(BadWordNamingWarning)})]
public class BadWordNamingAnalyzer : ElementProblemAnalyzer<IComment>
{
    protected override void Run(IComment element, ElementProblemAnalyzerData data,
        IHighlightingConsumer consumer)
    {
        var nodeText = element.CommentText;

        if (!nodeText.Contains("crap"))
            return;

        consumer.AddHighlighting(new BadWordNamingWarning(element, element.GetDocumentRange()));
    }
}