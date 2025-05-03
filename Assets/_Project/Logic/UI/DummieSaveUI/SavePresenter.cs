public class SavePresenter : IGameStartListener, IGameFinishListener
{
    private readonly SaveView _view;
    private readonly SaveService _saveService;

    public SavePresenter(SaveView view, SaveService saveService)
    {
        _view = view;
        _saveService = saveService;
    }


    public void OnGameStart()
    {
        _view.SaveButtonClicked += OnSaveButtonClicked;
        _view.LoadButtonClicked += OnLoadButtonClicked;
        _view.DeleteButtonClicked += OnDeleteButtonClicked;
    }

    private void OnDeleteButtonClicked() => 
            _saveService.Delete();

    private void OnLoadButtonClicked() => 
            _saveService.Load();

    private void OnSaveButtonClicked() => 
            _saveService.Save();

    public void OnGameFinish()
    {
        _view.SaveButtonClicked -= OnSaveButtonClicked;
        _view.LoadButtonClicked -= OnLoadButtonClicked;
        _view.DeleteButtonClicked -= OnDeleteButtonClicked;
    }
}