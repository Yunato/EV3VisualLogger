using System;
using System.Drawing;
using System.Windows.Forms;

namespace EV3VisualLogger.UI.View {
    public partial class OutsideTabControl : CustomTabControl {

        private int preIndex = -1;
        private int nextIndex = -1;
        private bool notIsDeletedTabPage = true;

        protected override void OnSelectedIndexChanged(EventArgs e) {
            base.OnSelectedIndexChanged(e);

            if (notIsDeletedTabPage) {
                preIndex = nextIndex;
                int nowIndex = this.SelectedIndex;

                if(preIndex > 0 && preIndex < this.TabPages.Count) {
                    OutsideTabPage tabPage = (OutsideTabPage)this.TabPages[preIndex];
                    Model.IO.FileStream fs = new Model.IO.FileStream(tabPage.Section);
                    fs.SaveFiles();
                }

                if (!this.DesignMode && this.SelectedIndex > -1 
                        && this.DisplayStyleProvider.ShowTabCloser 
                        && this.GetTabCloserRect(this.SelectedIndex).Contains(this.MousePosition)) {
                    if (preIndex > nowIndex) {
                        nextIndex = preIndex - 1;
                    }
                }else {
                    nextIndex = nowIndex > 0 ? nowIndex : 0;
                }
            }else {
                int nowIndex = nextIndex;
                if(preIndex > nowIndex) {
                    --preIndex;
                }
                nextIndex = preIndex;
            }

        }

        protected override void OnMouseClick(MouseEventArgs e) {
			int index = this.ActiveIndex;
            bool isNecessaryPostProcessing = false;
            if (!this.DesignMode && index > -1 
                    && this.DisplayStyleProvider.ShowTabCloser 
                    && this.GetTabCloserRect(index).Contains(this.MousePosition)) {
				TabPage tab = this.ActiveTab;
				TabControlCancelEventArgs args = new TabControlCancelEventArgs(tab, index, false, TabControlAction.Deselecting);
				this.OnTabClosing(args);

                if (!args.Cancel) {
                    notIsDeletedTabPage = false;

                    OutsideTabPage tabPage = (OutsideTabPage)this.TabPages[index];
                    Model.IO.FileStream fs = new Model.IO.FileStream(tabPage.Section);
                    fs.SaveFiles();

                    fileNames.Remove(fileNames[index]);
                }
                isNecessaryPostProcessing = true;
            }

            base.OnMouseClick(e);

            notIsDeletedTabPage = true;
            if (isNecessaryPostProcessing) {
                nextIndex = nextIndex > this.TabPages.Count - 1 ? nextIndex - 1 : nextIndex;
                this.SelectedIndex = nextIndex;
                int width = (this.Width - 50) / this.TabCount + 1;
                width = width > 140 ? 140 : width;
                this.ItemSize = new Size(width, 20);
            }
        }

        private void OutsideTabControl_OnSelectedIndexChanged(object sender, EventArgs e) {
            var control = (OutsideTabControl)sender;
        }

        private void OutsideTabControl_TabClosing(object sender, TabControlCancelEventArgs e) {
            var control = (OutsideTabControl)sender;
            if (control.TabCount <= 1) {
                e.Cancel = true;
            }
        }
    }
}
