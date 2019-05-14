using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Shouldly;
using Synker.Application.PlaylistFormaters;

namespace Synker.Application.Tests
{
    public class DefaultFormatterFactoryTest
    {
        [Fact]
        public void IsM3UFormater()
        {
            var factory = new DefaultFormatterFactory();
            var formater = factory.Create("m3u");
            formater.ShouldBeAssignableTo<M3UFormater>();
        }

        [Fact]
        public void IsTvListFormater()
        {
            var factory = new DefaultFormatterFactory();
            var formater = factory.Create("tvlist");
            formater.ShouldBeAssignableTo<TvListFormater>();
        }
    }
}
