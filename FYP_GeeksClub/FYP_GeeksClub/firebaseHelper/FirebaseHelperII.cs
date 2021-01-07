using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Query;
using Firebase.Storage;
using FYP_GeeksClub.Form;

namespace FYP_GeeksClub.firebaseHelper
{
    public class FirebaseHelperII
    {
        FirebaseClient firebaseClient = new FirebaseClient("https://hareware-59ccb.firebaseio.com/");
        FirebaseStorage firebaseStorage = new FirebaseStorage("hareware-59ccb.appspot.com");

        public async void PustPost(int id, string PostContect, string PostOwner, string ownerNmae, string ownerImage, string firstImage)
        {
            await firebaseClient.Child("Post").PostAsync(new PostDetail()
            {
                id = id,
                PostContect = PostContect,
                PostOwner = PostOwner,
                ownername = ownerNmae,
                ownerImage = ownerImage,
                firstImage = firstImage,
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

        public async Task<List<PostImageURL>> getPostImage(int postID)
        {
            return (await firebaseClient.Child("PostImageURL").Child(postID.ToString()).OnceAsync<PostImageURL>()).Select(image => new PostImageURL
            {
                PostID = image.Object.PostID,
                ImageURL = image.Object.ImageURL
            }).ToList();
        }

        public async Task<string> UploadPostImage(Stream fileStream, string title, int id)
        {
            var fileName = (title);
            var imageURL = await firebaseStorage.Child("PostImage").Child(id.ToString()).Child(fileName).PutAsync(fileStream);
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
                rePostImage = rePostImg
            });
        }

    }
    
}
