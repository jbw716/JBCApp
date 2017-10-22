﻿using System;
using System.Collections.Generic;

using Xamarin.Forms;

using JBC.TabbedPages;

namespace JBC
{
    public partial class Notes : ContentPage
	{
		public String originSource = "http://jbcpc.org/sermon_notes/";

        public Notes()
        {
			InitializeComponent();

            //Lambda expression for handling navigating to a Notes page.
            //directoryView.Navigated += Handle_Navigated;
			directoryView.Navigated += (s, e) => {
                
                String poundUrl = "http://jbcpc.org/sermon_notes/#";
                if (e.Url.Equals(poundUrl) || e.Url.Equals(originSource))
                {
                    bool loadPage = true;
                    directoryView.Navigating += async (sender, eArg) =>
                    {

                        //String searchUrlString = "http://jbcpc.org/sermon_notes/#search=";
                        //String filesUrlString = "http://jbcpc.org/sermon_notes/#files";
                        String ignoreUrl = "http://jbcpc.org/sermon_notes/#";
                        String requireUrl = "http://jbcpc.org/sermon_notes/files/";
                        String currUrl = directoryView.Source.ToString();
                        //if (!(eArg.Url.StartsWith(searchUrlString)) && !(eArg.Url.StartsWith(filesUrlString)) && (eArg.Url != "http://jbcpc.org/sermon_notes/"))
                        if (!(eArg.Url.StartsWith(ignoreUrl)) && (eArg.Url.StartsWith(requireUrl)) && loadPage)
						{
                            loadPage = false;
                            eArg.Cancel = true;
                            var uri = new Uri(eArg.Url);
							await Navigation.PushAsync(new Notes_View(uri));
                            //if (!(directoryView.Source.ToString().Equals(originSource)))
							directoryView.Source = originSource;
                        }

                    };
                }

            };
		}

        void Handle_Navigated(object sender, WebNavigatedEventArgs e)
        {

            String requireUrl = "http://jbcpc.org/sermon_notes/#";
            if(e.Url.Equals(requireUrl) ^ e.Url.Equals("http://jbcpc.org/sermon_notes/"))
                directoryView.Navigating += Handle_Navigating;

        }

        async void Handle_Navigating(object sender, WebNavigatingEventArgs e)
		{

			//eArg.Cancel = true;
			//String searchUrlString = "http://jbcpc.org/sermon_notes/#search=";
			//String filesUrlString = "http://jbcpc.org/sermon_notes/#files";
			String ignoreUrl = "http://jbcpc.org/sermon_notes/#";
			String requireUrl = "http://jbcpc.org/sermon_notes/files/";
            //if (!(eArg.Url.StartsWith(searchUrlString)) && !(eArg.Url.StartsWith(filesUrlString)) && (eArg.Url != "http://jbcpc.org/sermon_notes/"))
            if (!(e.Url.StartsWith(ignoreUrl)) && (e.Url.StartsWith(requireUrl)))
			{

				e.Cancel = true;
				var uri = new Uri(e.Url);
				await Navigation.PushAsync(new Notes_View(uri));
                directoryView.Source = originSource;

			}

        }
    }
}
