namespace Synker.Application.DataSources.Queries.GetListDatasource
{
    using MediatR;
    using System;
    using System.Collections.Generic;
    using System.Text;
    public class ListDatasourceQuery : IRequest<ListDatasourceViewModel>
    {
        public string Name { get; set; }

        public bool? Enabled { get; set; }

        public DateTime? CreatedFrom { get; set; }
    }
}
