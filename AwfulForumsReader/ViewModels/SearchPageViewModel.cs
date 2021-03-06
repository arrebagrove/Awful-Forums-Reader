﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AwfulForumsLibrary.Entity;
using AwfulForumsLibrary.Manager;
using AwfulForumsReader.Commands;
using AwfulForumsReader.Commands.Navigation;
using AwfulForumsReader.Common;
using AwfulForumsReader.Pages;
using AwfulForumsReader.Tools;

namespace AwfulForumsReader.ViewModels
{
    public class SearchPageViewModel : NotifierBase
    {
        public SearchPageViewModel()
        {
            if (ForumGroupList != null) return;
            ForumGroupList = new ObservableCollection<ForumCategoryEntity>();
        }
        private bool _isLoading;
        private SearchManager _searchManager;
        private string _searchTerms;
        private readonly ForumManager _forumManager = new ForumManager();
        private ObservableCollection<ForumCategoryEntity> _forumCategoryEntities;
        public NavigateToThreadPageViaSearchResult NavigateToThreadPageViaSearchResult { get; set; } = new NavigateToThreadPageViaSearchResult();
        public NavigateToSearchResultsCommand NavigateToSearchResultsCommand { get; set; } = new NavigateToSearchResultsCommand();
        public NavigateToSearchResultsFromListViewCommand NavigateToSearchResultsFromListViewCommand { get; set; } = new NavigateToSearchResultsFromListViewCommand();
        private SearchPageScrollingCollection _searchPageScrollingCollection;
        public SearchPageScrollingCollection SearchPageScrollingCollection
        {
            get { return _searchPageScrollingCollection; }
            set
            {
                SetProperty(ref _searchPageScrollingCollection, value);
                OnPropertyChanged();
            }
        }

        public ObservableCollection<ForumCategoryEntity> ForumGroupList
        {
            get { return _forumCategoryEntities; }
            set
            {
                SetProperty(ref _forumCategoryEntities, value);
                OnPropertyChanged();
            }
        }

        public string SearchTerms
        {
            get { return _searchTerms; }
            set
            {
                SetProperty(ref _searchTerms, value);
                OnPropertyChanged();
            }
        }

        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                SetProperty(ref _isLoading, value);
                OnPropertyChanged();
            }
        }

        public async Task Initialize()
        {
            IsLoading = true;
            try
            {
                _searchManager = new SearchManager();
                var forumCategoryEntities = await _forumManager.GetForumCategoryMainPage();
                foreach (var forumCategoryEntity in forumCategoryEntities)
                {
                    ForumGroupList.Add(forumCategoryEntity);
                }
            }
            catch (Exception ex)
            {
                AwfulDebugger.SendMessageDialogAsync("Failed to get forums list", ex);
            }
            IsLoading = false;
        }

        public async Task GetSearchResults(List<int> forumIds)
        {
            IsLoading = true;
            try
            {
                SearchPageScrollingCollection = new SearchPageScrollingCollection(forumIds, SearchTerms);
                App.RootFrame.Navigate(typeof(SearchResultsPage));
            }
            catch (Exception ex)
            {

                AwfulDebugger.SendMessageDialogAsync("Failed to get results", ex);
            }
            IsLoading = false;
        }

    }
}
