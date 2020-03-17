using Vnit.ApplicationCore.Entities.Common;

namespace Vnit.UnitTests.Builders
{
    public class AddressBuilder
    {
        private Address _address;
        public string TestStreet => "123 Main St.";
        public string TestCity => "Kent";
        public int TestState => 1;
        public Country TestCountry = new Country(){ Name = "USA" , Id = 1}; 
        public string TestZipCode => "44240";

        public AddressBuilder()
        {
            _address = WithDefaultValues();
        }
        public Address Build()
        {
            return _address;
        }
        public Address WithDefaultValues()
        {
            _address = new Address(TestStreet, TestCity, TestState, TestCountry.Id, TestZipCode);
            return _address;
        }
    }
}
