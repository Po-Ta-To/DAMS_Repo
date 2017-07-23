﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.Support.V7.Widget;
using Dental_IT.Droid.Adapters;

namespace Dental_IT.Droid.Fragments
{
    public class SearchTreatmentFragment : Android.Support.V4.App.Fragment
    {
        private Treatment a = new Treatment(1, "Treatment 1", 100, 500);
        private Treatment b = new Treatment(2, "Treatment 2", 250, 890);
        private Treatment c = new Treatment(3, "Treatment 3", 1230, 3450);
        private Treatment d = new Treatment(4, "Treatment 4", 250, 690);
        private Treatment e = new Treatment(5, "Treatment 5", 500, 1000);
        private Treatment f = new Treatment(6, "Treatment 6", 670, 2000);
        private Treatment g = new Treatment(7, "Treatment 7", 380, 600);
        private Treatment h = new Treatment(8, "Treatment 8", 450, 700);
        private Treatment i = new Treatment(9, "Treatment 9", 2000, 3460);
        private Treatment j = new Treatment(10, "Treatment 10", 570, 940);
        private Treatment k = new Treatment(11, "Treatment 11", 330, 450);
        private Treatment l = new Treatment(12, "Treatment 12", 550, 640);
        private Treatment m = new Treatment(13, "Treatment 13", 800, 850);

        private List<Treatment> treatmentList = new List<Treatment>();
        private RecyclerView searchTreatment_RecyclerView;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            treatmentList.Add(a);
            treatmentList.Add(b);
            treatmentList.Add(c);
            treatmentList.Add(d);
            treatmentList.Add(e);
            treatmentList.Add(f);
            treatmentList.Add(g);
            treatmentList.Add(h);
            treatmentList.Add(i);
            treatmentList.Add(j);
            treatmentList.Add(k);
            treatmentList.Add(l);
            treatmentList.Add(m);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            if (container == null)
            {
                return null;
            }

            View v = inflater.Inflate(Resource.Layout.Search_Treatment, container, false);

            searchTreatment_RecyclerView = v.FindViewById<RecyclerView>(Resource.Id.searchTreatment_RecyclerView);

            //  Configure custom adapter for recyclerview
            searchTreatment_RecyclerView.SetLayoutManager(new LinearLayoutManager(Activity));

            RecyclerViewAdapter_SearchTreatment adapter = new RecyclerViewAdapter_SearchTreatment(Activity, treatmentList);
            searchTreatment_RecyclerView.SetAdapter(adapter);

            return v;
        }
    }
}