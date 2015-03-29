﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AwfulForumsReader.Commands;
using AwfulForumsReader.Common;
using AwfulForumsLibrary.Entity;
using AwfulForumsReader.Tools;

namespace AwfulForumsReader.ViewModels
{
    public class ThreadListPageViewModel : NotifierBase
    {
        public ForumEntity ForumEntity { get; set; }
        private string _forumTitle;
        private ObservableCollection<ForumEntity> _subForumEntities;
        private PageScrollingCollection _forumPageScrollingCollection;
        private AddOrRemoveBookmarkCommand _addOrRemoveBookmarkCommand = new AddOrRemoveBookmarkCommand();
        private UnreadThreadCommand _unreadThreadCommand = new UnreadThreadCommand();
        private LastPageCommand _lastPageCommand = new LastPageCommand();
        private RefreshThreadListCommand _refreshThreadListCommand = new RefreshThreadListCommand();
        private NavigateToNewThreadCommand _navigateToNewThreadCommand = new NavigateToNewThreadCommand();
        private NavigateToThreadPageCommand _navigateToThreadPageCommand = new NavigateToThreadPageCommand();
        private NavigateToLastPageInThreadPageCommand _navigateToLastPageInThreadPageCommand = new NavigateToLastPageInThreadPageCommand();
        public NavigateToSubforumThreadListPageCommand NavigateToThreadListPageCommand { get; set; }  = new NavigateToSubforumThreadListPageCommand();
        public NavigateToLastPageInThreadPageCommand NavigateToLastPageInThreadPageCommand
        {
            get { return _navigateToLastPageInThreadPageCommand; }
            set { _navigateToLastPageInThreadPageCommand = value; }
        }

        public NavigateToThreadPageCommand NavigateToThreadPageCommand
        {
            get { return _navigateToThreadPageCommand; }
            set { _navigateToThreadPageCommand = value; }
        }

        public NavigateToNewThreadCommand NavigateToNewThreadCommand
        {
            get { return _navigateToNewThreadCommand; }
            set { _navigateToNewThreadCommand = value; }
        }

        public RefreshThreadListCommand RefreshThreadListCommand
        {
            get { return _refreshThreadListCommand; }
            set { _refreshThreadListCommand = value; }
        }

        public string ForumTitle
        {
            get { return _forumTitle; }
            set
            {
                SetProperty(ref _forumTitle, value);
                OnPropertyChanged();
            }
        }

        public AddOrRemoveBookmarkCommand AddOrRemoveBookmark
        {
            get { return _addOrRemoveBookmarkCommand; }
            set
            {
                SetProperty(ref _addOrRemoveBookmarkCommand, value);
                OnPropertyChanged();
            }
        }

        public UnreadThreadCommand UnreadThread
        {
            get { return _unreadThreadCommand; }
            set
            {
                SetProperty(ref _unreadThreadCommand, value);
                OnPropertyChanged();
            }
        }

        public LastPageCommand LastPage
        {
            get { return _lastPageCommand; }
            set
            {
                SetProperty(ref _lastPageCommand, value);
                OnPropertyChanged();
            }
        }

        public PageScrollingCollection ForumPageScrollingCollection
        {
            get { return _forumPageScrollingCollection; }
            set
            {
                SetProperty(ref _forumPageScrollingCollection, value);
                OnPropertyChanged();
            }
        }

        public ObservableCollection<ForumEntity> SubForumEntities
        {
            get { return _subForumEntities; }
            set
            {
                SetProperty(ref _subForumEntities, value);
                OnPropertyChanged();
            }
        }

        public void Initialize(ForumEntity forumEntity)
        {
            SubForumEntities = new ObservableCollection<ForumEntity>();
            ForumEntity = forumEntity;
            ForumTitle = forumEntity.Name;
            Refresh();
        }

        public void Refresh()
        {
            ForumPageScrollingCollection = new PageScrollingCollection(ForumEntity, 1);
        }

        public void UpdateThreadList()
        {
            OnPropertyChanged("ForumPageScrollingCollection");
        }
    }
}
