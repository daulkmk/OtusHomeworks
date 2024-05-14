namespace ShootEmUp.PresentationModel
{
    internal interface IPopup
    {
        void Show(IPopupPresenter presenter);
        void Hide();
    }
}