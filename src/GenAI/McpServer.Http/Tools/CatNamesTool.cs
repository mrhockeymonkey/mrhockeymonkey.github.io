using System.ComponentModel;
using ModelContextProtocol.Server;

namespace McpServer.Http.Tools;

[McpServerToolType]
public class CatNamesTool
{
    [McpServerTool, Description("Get a good name for a cat")]
    public static string GetCatName()
    {
        return "mr. fluffy";
    }
}