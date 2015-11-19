using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MapStrat_Luca.Controllers
{
    public class CustomerController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<Models.Customer> Get()
        {
            using (var db = new Models.MapStratDBEntities())
            {
                return db.Customers.ToArray();
            }
        }

        // GET api/<controller>/5
        public Models.Customer Get(int id)
        {
            try
            {
                using (var db = new Models.MapStratDBEntities())
                {
                    var customer = db.Customers.FirstOrDefault(f => f.Id == id);

                    if (customer == null)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No Customer found with ID = {0}", id))
                        };
                        throw new HttpResponseException(resp);
                    }
                    return customer;
                }
            }
            catch (Exception ex)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(ex.Message)
                };
                throw new HttpResponseException(resp);
            }
        }

        // PUT api/<controller>
        public Models.Customer Post([FromBody]Models.Customer value)
        {
            {
                try
                {
                    using (var db = new Models.MapStratDBEntities())
                    {
                        db.Customers.Add(value);

                        db.SaveChanges();

                        return value;
                    }
                }
                catch (Exception ex)
                {
                    var resp = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                    {
                        Content = new StringContent(ex.Message)
                    };
                    throw new HttpResponseException(resp);
                }
            }
        }

        // PUT api/<controller>/5
        public Models.Customer Put(int id, [FromBody]Models.Customer value)
        {
            try
            {
                using (var db = new Models.MapStratDBEntities())
                {
                    var customer = db.Customers.FirstOrDefault(f => f.Id == id);

                    if (customer == null)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No Customer found with ID = {0}", id))
                        };
                        throw new HttpResponseException(resp);
                    }

                    customer.Name = value.Name;
                    customer.Address = value.Address;
                    customer.PhoneNumber = value.PhoneNumber;

                    db.SaveChanges();

                    return customer;
                }
            }
            catch (Exception ex)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(ex.Message)
                };
                throw new HttpResponseException(resp);
            }
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
            try
            {
                using (var db = new Models.MapStratDBEntities())
                {
                    var customer = db.Customers.FirstOrDefault(f => f.Id == id);

                    if (customer == null)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent(string.Format("No Customer found with ID = {0}", id))
                        };
                        throw new HttpResponseException(resp);
                    }

                    db.Customers.Remove(customer);

                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(ex.Message)
                };
                throw new HttpResponseException(resp);
            }
        }
    }
}
