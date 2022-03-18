using BlazorComponent;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Masa.Blazor
{
    public class MDatePickerHeader : BDatePickerHeader, IThemeable, IDatePickerHeader
    {
        [Parameter]
        public bool Disabled { get; set; }

        [Parameter]
        public string Color { get; set; } = "accent";

        [Parameter]
        public DateOnly? Min { get; set; }

        [Parameter]
        public DateOnly? Max { get; set; }

        [Parameter]
        public EventCallback<DateOnly> OnInput { get; set; }

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
        public EventCallback OnToggle { get; set; }

        [Parameter]
        public string PrevIcon { get; set; }

        [Parameter]
        public bool Readonly { get; set; }

        [Parameter]
        public string NextIcon { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public Func<DateOnly, string> Format { get; set; }

        [Parameter]
        public DatePickerType ActivePicker { get; set; }

        [Parameter]
        public string Locale { get; set; }

        [Inject]
        public MasaBlazor MasaBlazor { get; set; }

        protected bool RTL => MasaBlazor.RTL;

        protected bool IsReversing { get; set; }

        protected string Transition => IsReversing == !MasaBlazor.RTL ? "tab-reverse-transition" : "tab-transition";

        protected Dictionary<string, object> ButtonAttrs
        {
            get
            {
                var attrs = new Dictionary<string, object>()
                {
                    { "type", "button" }
                };

                if (OnToggle.HasDelegate)
                {
                    attrs.Add("onclick", CreateEventCallback<MouseEventArgs>(HandleOnClickAsync));
                }

                return attrs;
            }
        }

        protected Func<DateOnly, string> Formatter
        {
            get
            {
                if (Format != null)
                {
                    return Format;
                }

                return ActivePicker == DatePickerType.Date ? DateFormatters.Date(Locale) : DateFormatters.Year(Locale);
            }
        }

        bool IDatePickerHeader.RTL => RTL;

        string IDatePickerHeader.Transition => Transition;

        Dictionary<string, object> IDatePickerHeader.ButtonAttrs => ButtonAttrs;

        Func<DateOnly, string> IDatePickerHeader.Formatter => Formatter;

        public DateOnly CalculateChange(int sign)
        {
            if (ActivePicker == DatePickerType.Month)
            {
                var date = Value.AddYears(sign);
                return new DateOnly(date.Year, 1, 1);
            }

            return MonthChange(Value, sign);
        }

        public static DateOnly MonthChange(DateOnly value, int sign)
        {
            var date = value.AddMonths(sign);
            return new DateOnly(date.Year, date.Month, 1);
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            Watcher
                .Watch<DateOnly>(nameof(Value), (newVal, oldVal) =>
                {
                    IsReversing = newVal < oldVal;
                });
        }

        protected override void SetComponentClass()
        {
            CssProvider
                .Apply(cssBuilder =>
                {
                    cssBuilder
                        .Add("m-date-picker-header")
                        .AddIf("m-date-picker-header--disabled", () => Disabled)
                        .AddTheme(IsDark);
                })
                .Apply("value", cssBuilder =>
                {
                    cssBuilder
                        .Add("m-date-picker-header__value")
                        .AddIf("m-date-picker-header__value--disabled", () => Disabled);
                })
                .Apply("header", cssBuilder =>
                {
                    var color = !Disabled ? (Color ?? "accent") : "";
                    cssBuilder
                        .AddTextColor(color);
                }, styleBuilder =>
                {
                    var color = !Disabled ? (Color ?? "accent") : "";
                    styleBuilder
                        .AddTextColor(color);
                });

            AbstractProvider
                .ApplyDatePickerHeaderDefault()
                .Apply<BButton, MButton>(attrs =>
                {
                    var change = attrs.Index;
                    var calculateChange = CalculateChange(change);
                    var disabled = Disabled || (change < 0 && Min != null && calculateChange < Min) || (change > 0 && Max != null && calculateChange > Max);

                    attrs[nameof(MButton.Dark)] = Dark;
                    attrs[nameof(MButton.Disabled)] = disabled;
                    attrs[nameof(MButton.Icon)] = true;
                    attrs[nameof(MButton.Light)] = Light;

                    if (OnInput.HasDelegate)
                    {
                        attrs[nameof(MButton.StopPropagation)] = true;
                        attrs[nameof(MButton.OnClick)] = CreateEventCallback<MouseEventArgs>(async args =>
                        {
                            await OnInput.InvokeAsync(calculateChange);
                        });
                    }
                })
                .Apply<BIcon, MIcon>();
        }

        private async Task HandleOnClickAsync(MouseEventArgs args)
        {
            await OnToggle.InvokeAsync();
        }
    }
}
