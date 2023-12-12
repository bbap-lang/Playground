using BBAP;
using BBAP.Config;
using BBAP.Results;
using Microsoft.AspNetCore.Components;

namespace BbapPlayground.Services;

public class CompilerService {
    public MarkupString Compile(string code) {
        Result<string> result = Compiler.Run(code, "/", new ConfigData());
        if (!result.TryGetValue(out var abap)) {
            return new MarkupString($"<span class=\"error\">Error at line {result.Error.Line}: {result.Error.Text}</span>");
        }
        
        
        var html = abap.Replace("\n", "<br>").Replace(" ", "&nbsp;");
        return new MarkupString(html);
    }
}