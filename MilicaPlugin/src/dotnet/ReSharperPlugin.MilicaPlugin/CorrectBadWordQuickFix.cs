using System;
using JetBrains.Annotations;
using JetBrains.Application.Progress;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Feature.Services.QuickFixes;
using JetBrains.ReSharper.Psi.CSharp;
using JetBrains.ReSharper.Psi.ExtensionsAPI;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.ReSharper.Resources.Shell;
using JetBrains.TextControl;
using JetBrains.Util;

namespace ReSharperPlugin.MilicaPlugin.Highlighter;

[QuickFix]
public class CorrectBadWordQuickFix : QuickFixBase
{
    private readonly IComment _comment;

    public CorrectBadWordQuickFix([NotNull] BadWordNamingWarning warning)
    {
        _comment = warning.Comment;
    }


    protected override Action<ITextControl> ExecutePsiTransaction(ISolution solution, IProgressIndicator progress)
    {
        var oldComment = _comment;
        var factory = CSharpElementFactory.GetInstance(oldComment);
        var newComment = factory.CreateComment(ConvertText(oldComment.GetText()));

        using (WriteLockCookie.Create())
        {
            LowLevelModificationUtil.ReplaceChild(oldComment, newComment);
        }

        return null;
    }

    private string ConvertText(string bad)
    {
        return "//amgood";
    }

    public override string Text => "Replace the bad word";

    public override bool IsAvailable(IUserDataHolder cache)
    {
        return _comment.IsValid();
    }
}