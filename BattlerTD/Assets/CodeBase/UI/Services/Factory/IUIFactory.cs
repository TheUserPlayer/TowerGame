using System.Collections.Generic;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.UI.Elements;
using CodeBase.UI.Menu;
using UnityEngine;

namespace CodeBase.UI.Services.Factory
{
  public interface IUIFactory: IService
  {
    void CreateUIRoot();
    void CreateShop();
    Transform UiRoot { get; }
    List<ISavedProgressReader> ProgressReaders { get; }
    List<ISavedProgress> ProgressWriters { get; }
    void CreateWinPanel();
    void CreateDeathPanel();
    MainMenu CreateMainMenu();
  }
}