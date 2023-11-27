using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PROG3050VideoGameStore.Models;
using PROG3050VideoGameStore.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PROG3050VideoGameStore.Controllers
{
    public class FriendsAndFamilyController : Controller
    {
        public FriendsAndFamilyController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }


        [HttpGet]
        public IActionResult AddFriendForm(int id = 0)
        {
            AddFriendsVM model = new AddFriendsVM();
            model.ProfileId = id;



            return View(model);
        }

        [HttpPost]
        public IActionResult AddFriendForm(AddFriendsVM model)
        {
            model.newFriend.ReceivedBy = 0;
            model.newFriend.Status = "Not yet accepted";
            model.newFriend.UserProfileId = model.ProfileId;
            if (ModelState.IsValid)
            {
                bool recepientExists = false;
                UserProfile recepientProfile = _appDbContext.Profiles.FirstOrDefault(p => p.DisplayName == model.newFriend.RecepientName);
                UserProfile senderProfile = _appDbContext.Profiles.Find(model.ProfileId);


                if (recepientProfile == null)
                {
                    recepientExists = false;
                }

                else
                {
                    recepientExists = true;
                }

                if (recepientExists == true)
                {
                    if (recepientProfile.DisplayName != senderProfile.DisplayName)
                    {

                        model.newFriend.ReceivedBy = recepientProfile.Id;
                        model.newFriend.RecepientName = recepientProfile.DisplayName;

                        FamilyFriendsList existingRequest = _appDbContext.FamilyFriendsList.FirstOrDefault(r => (r.UserProfileId == model.newFriend.UserProfileId && r.ReceivedBy == model.newFriend.ReceivedBy)||(r.UserProfileId == model.newFriend.ReceivedBy && r.ReceivedBy == model.newFriend.UserProfileId));

                        if (existingRequest != null)
                        {
                            if (existingRequest.Status == "Not yet accepted")
                            {
                                ViewData["Message"] = "One of you has already sent a request. The recipient needs to act upon the existing request.";
                                int tempId = model.ProfileId;
                                AddFriendsVM newModel = new AddFriendsVM();
                                newModel.ProfileId = tempId; ;

                                return View(newModel);
                            }

                            else
                            {
                                ViewData["Message"] = "Entered user is already on your friends list";
                                int tempId = model.ProfileId;
                                AddFriendsVM newModel = new AddFriendsVM();
                                newModel.ProfileId = tempId; ;

                                return View(newModel);
                            }
                        }

                        else
                        {
                            _appDbContext.FamilyFriendsList.Add(model.newFriend);
                            _appDbContext.SaveChanges();
                            ViewData["Message"] = "Friend request has been sent";

                            int tempId = model.ProfileId;
                            AddFriendsVM newModel = new AddFriendsVM();
                            newModel.ProfileId = tempId; ;

                            return View(newModel);
                        }
                    }
                    else
                    {
                        ViewData["Message"] = "Cannot enter your display name";
                        AddFriendsVM newModel = new AddFriendsVM();
                        newModel.ProfileId = model.ProfileId;
                        return View(newModel);
                    }

                }

                else
                {
                    ViewData["Message"] = "Entered user does not exist in the database";
                    return View(model);
                }





            }

            else
            {

                ViewData["Message"] = "";
                return View(model);
            }
        }

        public IActionResult FriendsAndFamily(int id = 0)
        {


            AllFriendsFamilyVM model = new AllFriendsFamilyVM();
            model.AllFriendsList = _appDbContext.FamilyFriendsList.Where(f => (f.UserProfileId == id || f.ReceivedBy == id) && f.Status == "Accepted").ToList();
            model.AllProfiles = _appDbContext.Profiles.ToList();
            model.ProfileId = id;

            return View("FriendsAndFamily", model);
        }

        public IActionResult FriendRequests(int id = 0)
        {


            AllFriendsFamilyVM model = new AllFriendsFamilyVM();
            model.AllFriendsList = _appDbContext.FamilyFriendsList.Where(f => f.ReceivedBy == id && f.Status == "Not yet accepted").ToList();
            model.AllProfiles = _appDbContext.Profiles.ToList();
            model.ProfileId = id;

            return View("FriendRequests", model);
        }




        public IActionResult RemoveFriend(int id = 0, int profileId = 0)
        {
            FamilyFriendsList alreadyExistingFriend = _appDbContext.FamilyFriendsList.Find(id);

            _appDbContext.FamilyFriendsList.Remove(alreadyExistingFriend);
            _appDbContext.SaveChanges();

            return RedirectToAction("FriendsAndFamily", "FriendsAndFamily", new { id = profileId });

        }

        public IActionResult DeclineRequest(int id = 0, int profileId = 0)
        {
            FamilyFriendsList alreadyExistingFriendRequest = _appDbContext.FamilyFriendsList.Find(id);

            _appDbContext.FamilyFriendsList.Remove(alreadyExistingFriendRequest);
            _appDbContext.SaveChanges();

            return RedirectToAction("FriendRequests", "FriendsAndFamily", new { id = profileId });

        }

        public IActionResult AcceptRequest(int id = 0, int profileId = 0)
        {
            FamilyFriendsList alreadyExistingFriendRequest = _appDbContext.FamilyFriendsList.Find(id);
            alreadyExistingFriendRequest.Status = "Accepted";

            _appDbContext.FamilyFriendsList.Update(alreadyExistingFriendRequest);
            _appDbContext.SaveChanges();

            return RedirectToAction("FriendRequests", "FriendsAndFamily", new { id = profileId });

        }

        private AppDbContext _appDbContext;
    }
}

