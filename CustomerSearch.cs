using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class CustomerSearch
{
    private readonly DatabaseContext db;

    public CustomerSearch(DatabaseContext dbContext)
    {
        db = dbContext;
    }

    public List<Customer> SearchByCountry(string country)
    {
        return SearchCustomers(c => c.Country.Contains(country));
    }

    public List<Customer> SearchByCompanyName(string companyName)
    {
        return SearchCustomers(c => c.CompanyName.Contains(companyName));
    }

    public List<Customer> SearchByContactPerson(string contactName)
    {
        return SearchCustomers(c => c.ContactName.Contains(contactName));
    }

    private List<Customer> SearchCustomers(Func<Customer, bool> searchCriteria)
    {
        var query = from customer in db.customers
                    where searchCriteria(customer)
                    orderby customer.CustomerID ascending
                    select customer;

        return query.ToList();
    }

    public string ExportToCSV(List<Customer> data)
    {
        StringBuilder sb = new StringBuilder();

        foreach (var item in data)
        {
            sb.AppendFormat("{0},{1},{2},{3}", item.CustomerID, item.CompanyName, item.ContactName, item.Country);
            sb.AppendLine();
        }

        return sb.ToString();
    }
}
