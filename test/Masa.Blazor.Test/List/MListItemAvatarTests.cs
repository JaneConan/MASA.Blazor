﻿using Bunit;

namespace Masa.Blazor.Test.List
{
    [TestClass]
    public class MListItemAvatarTests : TestBase
    {
        [TestMethod]
        public void RenderWithHeight()
        {
            // Act
            var cut = RenderComponent<MListItemAvatar>(props =>
            {
                props.Add(p => p.Height, 100);
            });
            var inputSlotDiv = cut.Find(".m-list-item__avatar");
            var style = inputSlotDiv.GetAttribute("style");

            // Assert
            Assert.AreEqual("height: 100px !important;", style);
        }

        [TestMethod]
        public void RenderListItemAvatarWithHorizontal()
        {
            //Act
            var cut = RenderComponent<MListItemAvatar>(props =>
            {
                props.Add(listitemavatar => listitemavatar.Horizontal, true);
            });
            var classes = cut.Instance.GetClass();
            var hasHorizontalClass = classes.Contains("m-list-item__avatar--horizontal");

            // Assert
            Assert.IsTrue(hasHorizontalClass);
        }

        [TestMethod]
        public void RenderListItemAvatarWithLeft()
        {
            //Act
            var cut = RenderComponent<MListItemAvatar>(props =>
            {
                props.Add(listitemavatar => listitemavatar.Left, true);
            });
            var classes = cut.Instance.GetClass();
            var hasLeftClass = classes.Contains("m-list-item__avatar");

            // Assert
            Assert.IsTrue(hasLeftClass);
        }

        [TestMethod]
        public void RenderWithMaxHeight()
        {
            // Act
            var cut = RenderComponent<MListItemAvatar>(props =>
            {
                props.Add(p => p.MaxHeight, 100);
            });
            var inputSlotDiv = cut.Find(".m-list-item__avatar");
            var style = inputSlotDiv.GetAttribute("style");

            // Assert
            Assert.AreEqual("max-height: 100px !important;", style);
        }

        [TestMethod]
        public void RenderWithMaxWidth()
        {
            // Act
            var cut = RenderComponent<MListItemAvatar>(props =>
            {
                props.Add(p => p.MaxWidth, 100);
            });
            var inputSlotDiv = cut.Find(".m-list-item__avatar");
            var style = inputSlotDiv.GetAttribute("style");

            // Assert
            Assert.AreEqual("max-width: 100px !important;", style);
        }

        [TestMethod]
        public void RenderWithMinHeight()
        {
            // Act
            var cut = RenderComponent<MListItemAvatar>(props =>
            {
                props.Add(p => p.MinHeight, 100);
            });
            var inputSlotDiv = cut.Find(".m-list-item__avatar");
            var style = inputSlotDiv.GetAttribute("style");

            // Assert
            Assert.AreEqual("min-height: 100px !important;", style);
        }

        [TestMethod]
        public void RenderWithMinWidth()
        {
            // Act
            var cut = RenderComponent<MListItemAvatar>(props =>
            {
                props.Add(p => p.MinWidth, 100);
            });
            var inputSlotDiv = cut.Find(".m-list-item__avatar");
            var style = inputSlotDiv.GetAttribute("style");

            // Assert
            Assert.AreEqual("min-width: 100px !important;", style);
        }

        [TestMethod]
        public void RenderListItemAvatarWithRight()
        {
            //Act
            var cut = RenderComponent<MListItemAvatar>(props =>
            {
                props.Add(listitemavatar => listitemavatar.Right, true);
            });
            var classes = cut.Instance.GetClass();
            var hasRightClass = classes.Contains("m-list-item__avatar");

            // Assert
            Assert.IsTrue(hasRightClass);
        }

        [TestMethod]
        public void RenderListItemAvatarWithRounded()
        {
            //Act
            var cut = RenderComponent<MListItemAvatar>(props =>
            {
                props.Add(listitemavatar => listitemavatar.Rounded, true);
            });
            var classes = cut.Instance.GetClass();
            var hasRoundedClass = classes.Contains("m-list-item__avatar");

            // Assert
            Assert.IsTrue(hasRoundedClass);
        }

        [TestMethod]
        public void RenderWithSize()
        {
            // Act
            var cut = RenderComponent<MListItemAvatar>(props =>
            {
                props.Add(p => p.Size, 100);
            });
            var inputSlotDiv = cut.Find(".m-list-item__avatar");
            var style = inputSlotDiv.GetAttribute("style");

            // Assert
            Assert.AreEqual("height: 100px !important;min-width: 100px !important;width: 100px !important;", style);
        }

        [TestMethod]
        public void RenderListItemAvatarWithTile()
        {
            //Act
            var cut = RenderComponent<MListItemAvatar>(props =>
            {
                props.Add(listitemavatar => listitemavatar.Tile, true);
            });
            var classes = cut.Instance.GetClass();
            var hasTileClass = classes.Contains("m-list-item__avatar");

            // Assert
            Assert.IsTrue(hasTileClass);
        }

        [TestMethod]
        public void RenderWithWidth()
        {
            // Act
            var cut = RenderComponent<MListItemAvatar>(props =>
            {
                props.Add(p => p.Width, 100);
            });
            var inputSlotDiv = cut.Find(".m-list-item__avatar");
            var style = inputSlotDiv.GetAttribute("style");

            // Assert
            Assert.AreEqual("width: 100px !important;", style);
        }

        [TestMethod]
        public void RenderWithChildContent()
        {
            // Arrange & Act
            var cut = RenderComponent<MListItemAvatar>(props =>
            {
                props.Add(listitemavatar => listitemavatar.ChildContent, "<span>Hello world</span>");
            });
            var contentDiv = cut.Find(".m-list-item__avatar");

            // Assert
            contentDiv.Children.MarkupMatches("<span>Hello world</span>");
        }
    }
}
