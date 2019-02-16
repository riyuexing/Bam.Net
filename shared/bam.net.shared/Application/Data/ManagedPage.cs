using Bam.Net.Configuration;
using Bam.Net.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bam.Net.Application.Data
{
    public class ManagedPage: CompositeKeyRepoData
    {
        public ManagedPage()
        {
            OrgnizationName = DefaultConfigurationOrganizationNameProvider.Instance.GetOrganizationName();
        }

        [CompositeKey]
        public string OrgnizationName { get; set; }

        [CompositeKey]
        public string ApplicationName { get; set; }

        [CompositeKey]
        public string PageName { get; set; }
    }
}
