using System;
using System.Collections.ObjectModel;
using SimplyMobile.Text;
using SimplyMobile.Text.ServiceStack;
using System.Text;

namespace WebClientTests
{
    public class ChartViewModel
    {
        public string Title { get; set; }

        public ObservableCollection<DataPoint> DataPoints { get; set; }

        //public string Data
        //{
        //    get
        //    {
        //        var builder = new StringBuilder();

        //        builder.Append("[");
        //        foreach (var point in DataPoints)
        //        {
        //            builder.Append("{");
        //            builder.Append(string.Format("label: \"{0}\", y: \"{1}\"", point.Label, point.Y));
        //            builder.Append("},");
        //        }
        //        builder.Append("]");

        //        return builder.ToString();
        //    }
        //}

        public ChartViewModel()
        {
            this.DataPoints = new ObservableCollection<DataPoint>();
        }

        public static ChartViewModel Dummy
        {
            get
            {
                var model = new ChartViewModel()
                {
                    Title = "Dummy model"
                };

                model.DataPoints.Add(new DataPoint() { Label = "Banana", Y = 18 });
                model.DataPoints.Add(new DataPoint() { Label = "Orange", Y = 29 });
                model.DataPoints.Add(new DataPoint() { Label = "Apple", Y = 40 });
                model.DataPoints.Add(new DataPoint() { Label = "Mango", Y = 34 });
                model.DataPoints.Add(new DataPoint() { Label = "Grape", Y = 24 });

                return model;
            }
        }
    }
}

