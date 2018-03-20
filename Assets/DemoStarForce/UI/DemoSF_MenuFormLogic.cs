using GameFramework;
using UnityGameFramework.Runtime;

public class DemoSF_MenuFormLogic : UIFormLogic {
    private DemoSF_ProcedureMenu m_ProcedureMenu;
    protected DemoSF_MenuFormLogic () { }

    protected override void OnOpen (object userData) {
        base.OnOpen (userData);

        // 打开UI的时候我们把DemoSF_ProcedureMenu作为参数传递了进去，在这里OnOpen事件会把它传递过来
        m_ProcedureMenu = (DemoSF_ProcedureMenu) userData;
        if (m_ProcedureMenu == null) {
            return;
        }
    }

    public void OnStarButtonClick() {
        Log.Debug("OnStarButtonClick");
        m_ProcedureMenu.StartGame();
    }
}