﻿using CommunityToolkit.Mvvm.Input;

using GenericViewModels.Services;

using System.ComponentModel;

namespace GenericViewModels.ViewModels;

public abstract class EditableItemViewModel<TItem> : ItemViewModel<TItem>, IEditableObject
    where TItem : class
{
    private readonly IItemsService<TItem> _itemsService;

    public EditableItemViewModel(IItemsService<TItem> itemsService)
        : base(itemsService.SelectedItem)
    {
        _itemsService = itemsService;

        PropertyChanged += (sender, e) =>
        {
            if (e.PropertyName == nameof(Item))
            {
                OnPropertyChanged(nameof(EditItem));
            }
        };

        EditCommand = new RelayCommand(BeginEdit, () => IsReadMode);
        CancelCommand = new RelayCommand(CancelEdit, () => IsEditMode);
        SaveCommand = new RelayCommand(EndEdit, () => IsEditMode);
        AddCommand = new RelayCommand(OnAdd, () => IsReadMode);
    }

    public RelayCommand AddCommand { get; }
    public RelayCommand EditCommand { get; }
    public RelayCommand CancelCommand { get; }
    public RelayCommand SaveCommand { get; }

    #region Edit / Read Mode
    private bool _isEditMode;
    public bool IsReadMode => !IsEditMode;
    public bool IsEditMode
    {
        get => _isEditMode;
        set
        {
            if (SetProperty(ref _isEditMode, value))
            {
                OnPropertyChanged(nameof(IsReadMode));
                CancelCommand.NotifyCanExecuteChanged();
                SaveCommand.NotifyCanExecuteChanged();
                EditCommand.NotifyCanExecuteChanged();
            }
        }
    }

    #endregion

    #region Copy Item for Edit Mode
    private TItem? _editItem;
    public TItem? EditItem
    {
        get => _editItem ?? Item;
        set => SetProperty(ref _editItem, value);
    }

    #endregion

    public abstract TItem CreateCopy(TItem item);

    #region Overrides Needed By Derived Class
    public abstract Task OnSaveAsync();
    public virtual Task OnEndEditAsync() => Task.CompletedTask;
    protected abstract void OnAdd();

    #endregion

    #region IEditableObject

    public virtual void BeginEdit()
    {
        if (Item is null) throw new InvalidOperationException("Item is null");

        IsEditMode = true;
        TItem itemCopy = CreateCopy(Item);
        if (itemCopy != null)
        {
            EditItem = itemCopy;
        }
    }

    public async virtual void CancelEdit()
    {
        IsEditMode = false;
        EditItem = default;
        await _itemsService.RefreshAsync();
        await OnEndEditAsync();
    }

    public async virtual void EndEdit()
    {
        using var _ = StartInProgress();
        await OnSaveAsync();
        EditItem = default;
        IsEditMode = false;
        await _itemsService.RefreshAsync();
        await OnEndEditAsync();
    }
    #endregion
}
