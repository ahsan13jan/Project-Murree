using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureManagement.Service
{
    static class LocationService
    {
        static FurnitureEntities context = new FurnitureEntities();
        public static void saveLocationOfficer(Location loc , Officer officer )
        {
            loc.IsDeleted = false;
            loc.CreatedAt = DateTime.Now;
            var added = context.Locations.Add(loc);
            context.SaveChanges();
            officer.Location = loc.Id;
            saveOfficer(officer);
        }

        public static void saveLocationUnit(Location loc, Unit unit)
        {
            loc.IsDeleted = false;
            loc.CreatedAt = DateTime.Now;
            var added = context.Locations.Add(loc);
            context.SaveChanges();
            unit.Location = loc.Id;
            saveunit(unit);
        }
        public static void deleteLocation(int id)
        {
            context.Locations.Find(id).IsDeleted = true;
            context.SaveChanges();
        }

        public static List<Location> getLocations()
        {
            var list =  Context.sharedInstance.Locations.Where(x => !x.IsDeleted).ToList();

            list.ForEach(x=> {
                x.Items = x.Items.Where(y => !y.IsDeleted).ToList();
            });

            return list;

        }
        public static bool isAssigned(int id)
        {
            return context.Locations.Find(id).Items.Count > 0;
        }



        public static void saveOfficer(Officer officer)
        {
            context.Officers.Add(officer);
            context.SaveChanges();
        }

        public static Officer getofficer(int locationId)
        {
            return context.Officers.Where(x => x.Location == locationId).FirstOrDefault();
        }

        public static Location getLocationById(int id)
        {
            return context.Locations.Find(id);
        }


        public static Officer getOfficerByLocationId(int locationid)
        {
            if (context.Locations.Find(locationid).Category == 2)

                return context.Officers.Where(x => x.Location == locationid && x.Date_Marchout == null).FirstOrDefault();

            else
                return null;
        }


        public static bool updatePreviousOficer(int id, DateTime dateMarchout)
        {
            Officer officer = context.Officers.Find(id);
            if (officer != null)
            {
                officer.Date_Marchout = dateMarchout;
                context.SaveChanges();
                return true;
            }
            else
                return false;
        }

        public static List<Officer> getOfficersByLocationId(int id)
        {
            return context.Officers.Where(x => x.Location == id).ToList();
        }

        public static void saveunit(Unit unit)
        {
            context.Units.Add(unit);
            context.SaveChanges();
        }

        public static List<Item> getAllItemByLocationId(int? locationId)
        {
            if (locationId == 0)
                locationId = null;
            return Context.sharedInstance.Items.Where(x => x.LocationID == locationId).ToList(); 
        }
    }
}
