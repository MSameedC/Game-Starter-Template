using UnityEngine.UIElements;

public abstract class MenuBase
{
    protected VisualElement root;

    // Track state explicitly to avoid UI Toolkit's uninitialized style traps
    public bool IsOpen { get; private set; }

    protected MenuBase(VisualElement root)
    {
        this.root = root;

        // Synchronize our initial state with whatever the layout currently is
        IsOpen = root.style.display == DisplayStyle.Flex;
    }

    // ---

    public virtual void Open()
    {
        root.style.display = DisplayStyle.Flex;
        IsOpen = true;
    }

    public virtual void Close()
    {
        root.style.display = DisplayStyle.None;
        IsOpen = false;
    }

    /// <summary>
    /// Toggles the display of the menu. Functions flawlessly with keybindings.
    /// </summary>
    public virtual void Toggle()
    {
        if (IsOpen)
            Close();
        else
            Open();
    }
}
