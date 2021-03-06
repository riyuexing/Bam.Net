using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Data;
using System.Data.Common;
using System.Linq;
using Bam.Net;
using Bam.Net.Data;
using Bam.Net.Data.Repositories;
using Newtonsoft.Json;
using Bam.Net.CoreServices.AssemblyManagement.Data;
using Bam.Net.CoreServices.AssemblyManagement.Data.Dao;

namespace Bam.Net.CoreServices.AssemblyManagement.Data.Wrappers
{
	// generated
	[Serializable]
	public class AssemblyReferenceDescriptorWrapper: Bam.Net.CoreServices.AssemblyManagement.Data.AssemblyReferenceDescriptor, IHasUpdatedXrefCollectionProperties
	{
		public AssemblyReferenceDescriptorWrapper()
		{
			this.UpdatedXrefCollectionProperties = new Dictionary<string, PropertyInfo>();
		}

		public AssemblyReferenceDescriptorWrapper(DaoRepository repository) : this()
		{
			this.Repository = repository;
		}

		[JsonIgnore]
		public DaoRepository Repository { get; set; }

		[JsonIgnore]
		public Dictionary<string, PropertyInfo> UpdatedXrefCollectionProperties { get; set; }

		protected void SetUpdatedXrefCollectionProperty(string propertyName, PropertyInfo correspondingProperty)
		{
			if(UpdatedXrefCollectionProperties != null && !UpdatedXrefCollectionProperties.ContainsKey(propertyName))
			{
				UpdatedXrefCollectionProperties.Add(propertyName, correspondingProperty);				
			}
			else if(UpdatedXrefCollectionProperties != null)
			{
				UpdatedXrefCollectionProperties[propertyName] = correspondingProperty;				
			}
		}




// Xref property: Left -> AssemblyDescriptor ; Right -> AssemblyReferenceDescriptor

		List<Bam.Net.CoreServices.AssemblyManagement.Data.AssemblyDescriptor> _assemblyDescriptors;
		public override List<Bam.Net.CoreServices.AssemblyManagement.Data.AssemblyDescriptor> AssemblyDescriptors
		{
			get
			{
				if(_assemblyDescriptors == null || _assemblyDescriptors.Count == 0)
				{
					var xref = new XrefDaoCollection<Bam.Net.CoreServices.AssemblyManagement.Data.Dao.AssemblyDescriptorAssemblyReferenceDescriptor, Bam.Net.CoreServices.AssemblyManagement.Data.Dao.AssemblyDescriptor>(Repository.GetDaoInstance(this), false);
					xref.Load(Repository.Database);
					_assemblyDescriptors = ((IEnumerable)xref).CopyAs<Bam.Net.CoreServices.AssemblyManagement.Data.AssemblyDescriptor>().ToList();
					SetUpdatedXrefCollectionProperty("AssemblyDescriptors", this.GetType().GetProperty("AssemblyDescriptors"));					
				}

				return _assemblyDescriptors;
			}
			set
			{
				_assemblyDescriptors = value;
				SetUpdatedXrefCollectionProperty("AssemblyDescriptors", this.GetType().GetProperty("AssemblyDescriptors"));
			}
		}	}
	// -- generated
}																								
