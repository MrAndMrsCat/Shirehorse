namespace Shirehorse.Core.Extensions
{
    public class NoGradient : ToolStripSystemRenderer
    {
        protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
        {   // fix issue with color gradient
            //base.OnRenderToolStripBorder(e);
        }
    }
}
