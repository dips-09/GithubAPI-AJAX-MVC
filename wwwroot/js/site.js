// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

var repo;
$(document).ready(function () {
    let address = "/api/user";
    $.ajax({
        type: "GET",
        dataType: "json",
        url: address,
        success: displayUserDetails,
        error: errorOnAjax
    });

    let address2 = "/api/repositories"
    $.ajax({
        type: "GET",
        dataType: "json",
        url: address2,
        success: displayRepositories,
        error: errorOnAjax
    });

    $("#btncommit").click(function () {
        let address3 = "api/commits/" + repo;
        $.ajax({
            type: "GET",
            dataType: "json",
            url: address3,
            success: displayCommits,
            error: errorOnAjax
        });
    });
});



function displayCommits(data) {
    $("#commits").Append("<h2>Commits in Repository</h2></br>");
    $("#commits").Append("<table> <tr><th>SHA</th><th>Timestamps</th><th>Committers</th><th>Commit Message</th></tr> ");
    for (let i = 0; i < data.Length; ++i) {
        $("#commits").Append("<tr><td>" + data[i].sha + "</td><td>" + data[i].timestamp + "</td><td>" + data[i].committer + "</td><td>" + data[i].message + "</td></tr>");
    }
    $("#commits").Append("</table>");
}

function errorOnAjax() {
    console.log("Error in ajax request");
}

function displayUserDetails(data) {
    console.log(data);
    $("#avatar").prepend($('<img>', { id: 'theImg', src: data.avatar_url, height: '100%', width: '100%' }));
    $("#description").html("<h2>" + data.name + "</h2></br>" + data.login + "</br>" + data.location);
}

function displayRepositories(data) {
    console.log(data);
    for (let i = 0; i < data.length; ++i) {
        
        repo = data[i].repoName;
        $("#RepoCards").append("<div class='card col-lg-3 col-sm-6 bg-success text-white' style='padding: 10px;margin-bottom : 20px;margin-top : 20px;margin-left : 20px;'>" + "<div class='card-body'><h5>" + data[i].repoName + "</h5><h6>"
            + "Updated " + data[i].lastUpdatedDays
            + " ago</h6>"
            + "<br/><button class='btn-info' id='btncommit'> Commits</button>"
            + "</div></div>");
    }
    
}