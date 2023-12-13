using System.Diagnostics;
using BBAP;
using BBAP.Config;
using BBAP.Results;
using Microsoft.AspNetCore.Components;

namespace BbapPlayground.Services;

public class CompilerService {
    public (MarkupString markupString, TimeSpan timeSpan) Compile(string code) {
        var sw = Stopwatch.StartNew();
        Result<string> result = Compiler.Run(code, "/", new ConfigData());
        sw.Stop();
        if (!result.TryGetValue(out var abap)) {
            return (new MarkupString($"<span class=\"error\">Error at line {result.Error.Line}: {result.Error.Text}</span>"), TimeSpan.Zero);
        }
        
        
        var html = abap.Replace("\n", "<br>").Replace(" ", "&nbsp;");
        return (new MarkupString(html), sw.Elapsed);
    }
}