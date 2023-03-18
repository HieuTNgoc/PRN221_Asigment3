"use strict";

var connection = new signalR.HubConnectionBuilder()
    .withUrl('/ChatHub')
    .build();

connection.on('ReceivePost', addPostToList);

connection.start().catch(function (err) {
    return console.error(err.toString());
});

class Post {
    constructor(PostId, AuthorId, CreatedDate, UpdateDate, Title, Content, PublishStatus, CategoryId) {
        this.PostId = PostId;
        this.AuthorId = AuthorId;
        this.CreatedDate = CreatedDate;
        this.UpdateDate = UpdateDate;
        this.Title = Title;
        this.Content = Content;
        this.PublishStatus = PublishStatus;
        this.CategoryId = CategoryId;
    }
}

const PostId = document.getElementById('PostId');
const AuthorId = document.getElementById('AuthorId');
const CreatedDate = document.getElementById('CreatedDate');
const UpdatedDate = document.getElementById('UpdatedDate');
const Title = document.getElementById('Title');
const Content = document.getElementById('Content');
const PublishStatus = document.getElementById('PublishStatus');
const CategoryId = document.getElementById('CategoryId');
const PostQueue = [];
const PostBox = document.getElementById('PostBox');

document.getElementById('SubmitButton').addEventListener('click', () => {
    let new_post = new Post(PostId.value, AuthorId.value, CreatedDate.value, UpdatedDate.value, Title.value, Content.value,  PublishStatus.value, CategoryId.value);
    PostQueue.push(new_post);
    console.log(PostQueue);
    clearInputField();
    sendPost();
});

function clearInputField() {
    Content.value = "";
    Title.value = "";
}

function sendPost() {
    console.log("seee nen");
    let comming_post = PostQueue.shift() || "";
    console.log(comming_post);
    if (comming_post === "") return;
    sendPostToHub(comming_post);
    console.log("Send nen");
}

function sendPostToHub(post) {
    connection.invoke('SendPost', post.PostId, post.AuthorId, post.Title, post.Content, post.PublishStatus, post.CategoryId).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
}

function addPostToList(postId, authorId, createdDate, updatedDate,title, content, publishStatus, categoryId) {
    console.log("addd nen");
    let post = new Post(postId, authorId, createdDate, updatedDate, title, content, publishStatus, categoryId);
    console.log(post);
    console.log("add xong");

    let container = document.createElement('div');
    let sender = document.createElement('p');
    sender.className = "sender";
    sender.innerHTML = post.AuthorId;
    let text = document.createElement('p');
    text.innerHTML = post.Content;

    container.appendChild(sender);
    container.appendChild(text);
    PostBox.appendChild(container);
}