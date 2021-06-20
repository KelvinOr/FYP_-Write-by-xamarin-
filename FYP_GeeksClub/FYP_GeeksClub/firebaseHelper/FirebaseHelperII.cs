using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Query;
using Firebase.Storage;
using FYP_GeeksClub.Form;
using Xamarin.Forms;

namespace FYP_GeeksClub.firebaseHelper
{
    public class FirebaseHelperII
    {
        FirebaseClient firebaseClient = new FirebaseClient(new APIKey().FirebaseClient);
        FirebaseStorage firebaseStorage = new FirebaseStorage(new APIKey().FirebaseStorage);

        public async void PustPost(int id, string PostContect, string PostOwner, string ownerName, string ownerImage, string firstImage, bool haveImage, bool haveMoreImg)
        {
            await firebaseClient.Child("Post").PostAsync(new PostDetail()
            {
                id = id,
                PostContect = PostContect,
                PostOwner = PostOwner,
                ownername = ownerName,
                ownerImage = ownerImage,
                firstImage = firstImage,
                haveMoreImg = haveMoreImg,
                haveImage = haveImage,
                Time = DateTime.Now.ToString("yyyyMMddHHmmssffff"),
                ShowTime = DateTime.Now.ToString()
            });
        }

        public async Task<List<PostDetail>> getAllPost()
        {
            return (await firebaseClient.Child("Post").OnceAsync<PostDetail>()).Select(post => new PostDetail
            {
                id = post.Object.id,
                PostContect = post.Object.PostContect,
                PostOwner = post.Object.PostOwner,
                ownername = post.Object.ownername,
                ownerImage = post.Object.ownerImage,
                firstImage = post.Object.firstImage,
                haveImage = post.Object.haveImage,
                haveMoreImg = post.Object.haveMoreImg,
                Time = post.Object.Time,
                ShowTime = post.Object.ShowTime
            }).OrderByDescending(a => a.Time).ToList();
        }

        public async void postTimeUpdate(int id)
        {
            var Check = (await firebaseClient.Child("Post").OnceAsync<PostDetail>()).Where(a => a.Object.id == id).FirstOrDefault();
            if (Check == null)
            {
                await firebaseClient.Child("Post").PostAsync(new PostDetail()
                {
                    id = Check.Object.id,
                    PostContect = Check.Object.PostContect,
                    PostOwner = Check.Object.PostOwner,
                    ownername = Check.Object.ownername,
                    ownerImage = Check.Object.ownerImage,
                    firstImage = Check.Object.firstImage,
                    haveImage = Check.Object.haveImage,
                    haveMoreImg = Check.Object.haveMoreImg,
                    Time = DateTime.Now.ToString("yyyyMMddHHmmssffff"),
                    ShowTime = DateTime.Now.ToString()
                });
            }
            else
            {
                var Update = (await firebaseClient.Child("Post").OnceAsync<PostDetail>()).Where(a => a.Object.id == id).FirstOrDefault();
                await firebaseClient.Child("Post").Child(Update.Key).PutAsync(new PostDetail()
                {
                    id = Check.Object.id,
                    PostContect = Check.Object.PostContect,
                    PostOwner = Check.Object.PostOwner,
                    ownername = Check.Object.ownername,
                    ownerImage = Check.Object.ownerImage,
                    firstImage = Check.Object.firstImage,
                    haveImage = Check.Object.haveImage,
                    haveMoreImg = Check.Object.haveMoreImg,
                    Time = DateTime.Now.ToString("yyyyMMddHHmmssffff"),
                    ShowTime = DateTime.Now.ToString()
                });
            }

        }

        public async Task<List<PostDetail>> GetPostbyEmail(string email)
        {
            return (await firebaseClient.Child("Post").OnceAsync<PostDetail>()).Where(a => a.Object.PostOwner == email).Select(post => new PostDetail
            {
                id = post.Object.id,
                PostContect = post.Object.PostContect,
                PostOwner = post.Object.PostOwner,
                ownername = post.Object.ownername,
                ownerImage = post.Object.ownerImage,
                firstImage = post.Object.firstImage,
                haveImage = post.Object.haveImage,
                haveMoreImg = post.Object.haveMoreImg,
                Time = post.Object.Time,
                ShowTime = post.Object.ShowTime
            }).OrderBy(a => a.Time).ToList();
        }

        public async void UploadPostImageURL(int postID, string imageURL)
        {
            await firebaseClient.Child("PostImageURL").Child(postID.ToString()).PostAsync(new PostImageURL()
            {
                PostID = postID,
                ImageURL = imageURL
            });
        }

        public async Task<List<PostImageURL>> getAllPostImg(int id)
        {
            return (await firebaseClient.Child("PostImageURL").Child(id.ToString()).OnceAsync<PostImageURL>()).Select(image => new PostImageURL
            {
                PostID = image.Object.PostID,
                ImageURL = image.Object.ImageURL
            }).ToList();
        }

        public async Task<string> getFirstImage(int postID)
        {
            var temp = (await firebaseClient.Child("PostImageURL").Child(postID.ToString()).OnceAsync<PostImageURL>()).FirstOrDefault();
            return temp.Object.ImageURL;
        }


        public async Task<string> UploadPostImage(Stream fileStream, string owner, int postId, int id)
        {
            var fileName = (owner + id);
            var imageURL = await firebaseStorage.Child("PostImage").Child(postId.ToString()).Child(fileName).PutAsync(fileStream);
            return imageURL;
        }

        public async void UploadRePost(int id, string detail, string rePostEmail, string rePostName, string rePostImg)
        {
            await firebaseClient.Child("RePost").Child(id.ToString()).PostAsync(new RePost()
            {
                PostID = id,
                rePost = detail,
                rePostUserEmail = rePostEmail,
                rePostUserName = rePostName,
                rePostUserImage = rePostImg,
                Time = DateTime.Now.ToString("yyyyMMddHHmmssffff"),
            });
        }

        public async Task<List<RePost>> getRePost(int id)
        {
            return (await firebaseClient.Child("RePost").Child(id.ToString()).OnceAsync<RePost>()).Where(a => a.Object.PostID == id).Select(post => new RePost
            {
                PostID = post.Object.PostID,
                rePost = post.Object.rePost,
                rePostUserEmail = post.Object.rePostUserEmail,
                rePostUserName = post.Object.rePostUserName,
                rePostUserImage = post.Object.rePostUserImage,
                Time = post.Object.Time
            }).OrderByDescending(a => a.Time).ToList();
        }

    }
    
}
