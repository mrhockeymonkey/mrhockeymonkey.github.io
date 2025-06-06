# Markdig

good for rendering openai output into html in Blazor

```html
<div class="markdown-output" style="white-space: pre-wrap;">
  @((MarkupString)_aiSearchResponseHtml)
</div>
```
```cs
await foreach (var part in SearchAssistant.GetStreamingResponse(_aiSearchTerm))
{
  _aiSearchResponse += part;
  _aiSearchResponseHtml = Markdig.Markdown.ToHtml(_aiSearchResponse);
  await InvokeAsync(StateHasChanged);
}
```

you can also extend it to take more control of rendering or parsing

```cs
var pipeline = new MarkdownPipelineBuilder()
  .Use<MyMarkdownExtension>()
  .Build();

_aiSearchResponseHtml = Markdig.Markdown.ToHtml(_aiSearchResponse, pipeline);
```
```cs
public class MyMarkdownExtension : IMarkdownExtension
{
    public void Setup(MarkdownPipelineBuilder pipeline)
    {
    }

    public void Setup(MarkdownPipeline pipeline, IMarkdownRenderer renderer)
    {
        if (renderer is not HtmlRenderer) return;

        // remove the built in code block renderer
        var codeBlockRenderer = renderer.ObjectRenderers.FindExact<CodeBlockRenderer>();

        if (codeBlockRenderer is not null)
        {
            renderer.ObjectRenderers.Remove(codeBlockRenderer);
        }
        else
        {
            codeBlockRenderer = new CodeBlockRenderer();
        }

        renderer.ObjectRenderers.AddIfNotAlready(
            new CustomCodeBlockRenderer(
                codeBlockRenderer
            )
        );
    }
}
```
```cs
public class CustomCodeBlockRenderer : HtmlObjectRenderer<CodeBlock>
{
    private readonly CodeBlockRenderer _underlyingCodeBlockRenderer;

    public CustomCodeBlockRenderer(CodeBlockRenderer underlyingCodeBlockRenderer)
    {
        _underlyingCodeBlockRenderer = underlyingCodeBlockRenderer;
    }
    
    protected override void Write(HtmlRenderer renderer, CodeBlock obj)
    {
        // renders using normal renderer but adds a button afterwards
        _underlyingCodeBlockRenderer.Write(renderer, obj);
        renderer.Write("""<button type="button" class="btn btn-primary" @onclick="AiSearch">Try It</button>""");
    }
}
```
