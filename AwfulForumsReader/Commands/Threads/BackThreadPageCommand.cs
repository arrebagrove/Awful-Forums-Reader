﻿using AwfulForumsReader.Common;

namespace AwfulForumsReader.Commands.Threads
{
    public class BackThreadPageCommand : AlwaysExecutableCommand
    {
        public async override void Execute(object parameter)
        {
            var vm = Locator.ViewModels.ThreadPageVm;
            if (vm.ForumThreadEntity.CurrentPage <= 1) return;
            vm.ForumThreadEntity.CurrentPage--;
            vm.ForumThreadEntity.ScrollToPost = 0;
            vm.ForumThreadEntity.ScrollToPostString = string.Empty;
            await Locator.ViewModels.ThreadPageVm.GetForumPostsAsync();
        }
    }
}
