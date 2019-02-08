onSignIn = function (googleUser) {
    
        var profile = googleUser.getBasicProfile();
        console.log('ID: ' + profile.getId()); // Do not send to your backend! Use an ID token instead.
        console.log('Name: ' + profile.getName());
        console.log('Image URL: ' + profile.getImageUrl());
        console.log('Email: ' + profile.getEmail());
        var id_token = googleUser.getAuthResponse().id_token;
        console.log("ID Token: " + id_token);

        var userName = profile.getName();
        var userEmail = profile.getEmail();
        var userTokenID = googleUser.getAuthResponse().id_token;

        $.ajax({
            type: "POST",
            url: "Account/LogIn",
            contentType: "application/json; charset=utf-8",
            data: { name: userName, email: userEmail, tockenID: userTokenID },
            dataType: "json",
            success: function (result) {
                alert('Yay! It worked!');
                // Or if you are returning something
                alert('I returned... ' + result.WhateverIsReturning);
            },
            error: function (result) {
                alert('Oh no :(');
            }
        });

    }
