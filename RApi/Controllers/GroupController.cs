using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using JsonData;

namespace MusicApi.Controllers {
    public class GroupController : Controller {
        List<Group> allGroups {get; set;}
        List<Artist> allArtists {get; set;}
        
        public GroupController() {
            allGroups = JsonToFile<Group>.ReadJson();
            allArtists = JsonToFile<Artist>.ReadJson();
            
        }

        [Route("groups")]
        [HttpGet]
        public JsonResult all_groups()
        {
            return Json(allGroups);
        }

        //?displayArtists=true
        [Route("groups/name/{name}")]
        [HttpGet]
        public JsonResult group_name(string name, bool displayArtists)
        {
            var found = allGroups.Where(group => group.GroupName.ToLower() == name.ToLower());
            if (displayArtists == true) {
                found = found.GroupJoin(allArtists,
                    //join by group id and artist group id
                    group => group.Id,
                    artist => artist.GroupId,
                    //display members as list
                    (group, artist) => {group.Members = artist.ToList(); return group;}).ToList();
            }
            return Json(found);
        }

        //?displayArtists=true
        [Route("groups/id/{id}")]
        [HttpGet]
        public JsonResult group_id(int id, bool displayArtists)
        {
            var found = allGroups.Where(group => group.Id == id);       
            if (displayArtists == true) {
                found = found.GroupJoin(allArtists,
                    group => group.Id,
                    artist => artist.GroupId,
                    (group, artist) => {group.Members = artist.ToList(); return group;}).ToList();
            }
            return Json(found);
        }
    }
}