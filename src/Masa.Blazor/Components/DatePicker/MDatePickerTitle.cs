﻿using BlazorComponent;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Masa.Blazor
{
    public partial class MDatePickerTitle : BDatePickerTitle, IDatePickerTitle
    {
        [Parameter]
        public DateOnly Value
        {
            get
            {
                return GetValue<DateOnly>();
            }
            set
            {
                SetValue(value);
            }
        }

        [Parameter]
        public bool Disabled { get; set; }

        [Parameter]
        public bool Readonly { get; set; }

        [Parameter]
        public bool SelectingYear { get; set; }

        [Parameter]
        public EventCallback<bool> OnSelectingYearUpdate { get; set; }

        [Parameter]
        public string Year { get; set; }

        [Parameter]
        public string YearIcon { get; set; }

        [Parameter]
        public string Date { get; set; }

        protected string ComputedTransition
        {
            get
            {
                return IsReversing ? "picker-reverse-transition" : "picker-transition";
            }
        }

        protected bool IsReversing { get; set; }

        string IDatePickerTitle.ComputedTransition => ComputedTransition;

        Task IDatePickerTitle.HandleOnTitleDateBtnClickAsync(MouseEventArgs args) => HandleOnTitleDateBtnClickAsync(args);

        Task IDatePickerTitle.HandleOnYearBtnClickAsync(MouseEventArgs args) => HandleOnYearBtnClickAsync(args);

        protected override void OnInitialized()
        {
            base.OnInitialized();

            Watcher
                .Watch<DateOnly>(nameof(Value), (val, prev) =>
                {
                    IsReversing = val < prev;
                });
        }

        protected override void SetComponentClass()
        {
            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-date-picker-title")
                        .AddIf("m-date-picker-title--disabled", () => Disabled);
                })
                .Apply("year-btn", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-picker__title__btn")
                        .Add("m-date-picker-title__year")
                        .AddIf("m-picker__title__btn--active", () => SelectingYear);
                })
                .Apply("title-date-btn", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-picker__title__btn")
                        .Add("m-date-picker-title__date")
                        .AddIf("m-picker__title__btn--active", () => !SelectingYear);
                });

            AbstractProvider
                .ApplyDatePickerTitleDefault()
                .Apply<BIcon, MIcon>(attrs =>
                {
                    attrs[nameof(MIcon.Dark)] = true;
                });
        }

        private async Task HandleOnYearBtnClickAsync(MouseEventArgs args)
        {
            var active = SelectingYear;
            if (active)
            {
                return;
            }

            if (OnSelectingYearUpdate.HasDelegate)
            {
                await OnSelectingYearUpdate.InvokeAsync(true);
            }
        }

        private async Task HandleOnTitleDateBtnClickAsync(MouseEventArgs args)
        {
            var active = !SelectingYear;
            if (active)
            {
                return;
            }

            if (OnSelectingYearUpdate.HasDelegate)
            {
                await OnSelectingYearUpdate.InvokeAsync(false);
            }
        }
    }
}
