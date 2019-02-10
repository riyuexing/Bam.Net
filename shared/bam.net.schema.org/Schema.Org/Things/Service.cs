using Bam.Net.Schema.Org.DataTypes;

namespace Bam.Net.Schema.Org.Things
{
	///<summary>A service provided by an organization, e.g. delivery service, print services, etc.</summary>
	public class Service: Intangible
	{
		///<summary>The overall rating, based on a collection of reviews or ratings, of the item.</summary>
		public AggregateRating AggregateRating {get; set;}
		///<summary>The geographic area where a service or offered item is provided. Supersedes serviceArea.</summary>
		public OneOfThese<AdministrativeArea,GeoShape,Place,Text> AreaServed {get; set;}
		///<summary>An intended audience, i.e. a group for whom something was created. Supersedes serviceAudience.</summary>
		public Audience Audience {get; set;}
		///<summary>A means of accessing the service (e.g. a phone bank, a web site, a location, etc.).</summary>
		public ServiceChannel AvailableChannel {get; set;}
		///<summary>An award won by or for this item. Supersedes awards.</summary>
		public Text Award {get; set;}
		///<summary>The brand(s) associated with a product or service, or the brand(s) maintained by an organization or business person.</summary>
		public OneOfThese<Brand,Organization> Brand {get; set;}
		///<summary>An entity that arranges for an exchange between a buyer and a seller.  In most cases a broker never acquires or releases ownership of a product or service involved in an exchange.  If it is not clear whether an entity is a broker, seller, or buyer, the latter two terms are preferred. Supersedes bookingAgent.</summary>
		public OneOfThese<Organization,Person> Broker {get; set;}
		///<summary>A category for the item. Greater signs or slashes can be used to informally indicate a category hierarchy.</summary>
		public OneOfThese<PhysicalActivityCategory,Text,Thing> Category {get; set;}
		///<summary>Indicates an OfferCatalog listing for this Organization, Person, or Service.</summary>
		public OfferCatalog HasOfferCatalog {get; set;}
		///<summary>The hours during which this service or contact is available.</summary>
		public OpeningHoursSpecification HoursAvailable {get; set;}
		///<summary>A pointer to another, somehow related product (or multiple products).</summary>
		public OneOfThese<Product,Service> IsRelatedTo {get; set;}
		///<summary>A pointer to another, functionally similar product (or multiple products).</summary>
		public OneOfThese<Product,Service> IsSimilarTo {get; set;}
		///<summary>An associated logo.</summary>
		public OneOfThese<ImageObject,Url> Logo {get; set;}
		///<summary>An offer to provide this item—for example, an offer to sell a product, rent the DVD of a movie, perform a service, or give away tickets to an event.</summary>
		public Offer Offers {get; set;}
		///<summary>The service provider, service operator, or service performer; the goods producer. Another party (a seller) may offer those services or goods on behalf of the provider. A provider may also serve as the seller. Supersedes carrier.</summary>
		public OneOfThese<Organization,Person> Provider {get; set;}
		///<summary>Indicates the mobility of a provided service (e.g. 'static', 'dynamic').</summary>
		public Text ProviderMobility {get; set;}
		///<summary>A review of the item. Supersedes reviews.</summary>
		public Review Review {get; set;}
		///<summary>The tangible thing generated by the service, e.g. a passport, permit, etc. Supersedes produces.</summary>
		public Thing ServiceOutput {get; set;}
		///<summary>The type of service being offered, e.g. veterans' benefits, emergency relief, etc.</summary>
		public Text ServiceType {get; set;}
		///<summary>Human-readable terms of service documentation.</summary>
		public OneOfThese<Text,Url> TermsOfService {get; set;}
	}
}
