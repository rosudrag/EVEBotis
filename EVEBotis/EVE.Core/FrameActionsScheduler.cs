namespace ILoveEVE.Core
{
  public class FrameActionsScheduler : IFrameActionsScheduler
  {
    private int frameCount;

    public FrameActionsScheduler(int frameCountToExecuteAt)
    {
      FrameCountToExecuteAt = frameCountToExecuteAt;
      frameCount = 0;
    }

    private int FrameCountToExecuteAt { get; set; }

    public bool TryExecute()
    {
      frameCount++;

      frameCount %= FrameCountToExecuteAt;

      return frameCount == 0;
    }
  }
}