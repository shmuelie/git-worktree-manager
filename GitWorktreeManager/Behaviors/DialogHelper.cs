﻿namespace GitWorktreeManager.Behaviors;

using GitWorktreeManager.Services;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Threading.Tasks;

internal static class DialogHelper
{
    public static async Task ShowErrorAsync(Exception exception)
    {
        if (exception is GitException gitException)
        {
            await ShowAsync(
                $"Git Error ({gitException.ExitCode})", 
                $"git {gitException.Command}"
                + Environment.NewLine
                + Environment.NewLine
                + gitException.Error);
        }
        else
        {
            await ShowAsync(exception.GetType().Name, exception.Message);
        }
    }

    private static async Task ShowAsync(string title, string details)
    {
        var noWifiDialog = new ContentDialog
        {
            // XamlRoot must be set in the case of a ContentDialog running in a Desktop app
            XamlRoot = MainWindow.Instance.Content.XamlRoot,

            Title = title,
            Content = details,
            CloseButtonText = "Ok"
        };

        await noWifiDialog.ShowAsync();
    }
}
