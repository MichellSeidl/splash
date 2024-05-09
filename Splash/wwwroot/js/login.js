async function login() {
    event.preventDefault();

    var validUser = true;
    var validPass = true;

    $( '#username' ).removeClass( 'is-invalid' );
    $( '#username' ).removeClass( 'is-valid' );
    $( '#password' ).removeClass( 'is-invalid' );
    $( '#password' ).removeClass( 'is-valid' );

    var user = document.getElementById('username').value
    if (user.trim() == "") {
        validUser = false;
        $('#username').addClass('is-invalid');
        document.getElementById( 'invalid_user_feed' ).innerText = "Usuário inválido!";
    } else {
        $('#username').addClass('is-valid');
        document.getElementById('valid_user_feed').innerText = "";
    }


    var pass = document.getElementById('password').value
    if (pass.trim() == "") {
        validPass = false;
        $('#password').addClass('is-invalid');
        document.getElementById('invalid_pass_feed').innerText = "Senha inválida!";
    } else {
        $('#password').addClass('is-valid');
        document.getElementById('valid_pass_feed').innerText = "";
    }

    if (validUser && validPass) {
        fetch('ValidateUser', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({
                username: user,
                password: pass,
            }),
        })
            .then(response => {
                if (!response.ok) {
                    throw new Error('Network response was not ok');
                }
                return response.json(); 
            })
            .then(data => {
                if (!data.isValid) {
                    document.getElementById('btn_login').innerHTML = "<div class='spinner-border'' role='status'></div >";
                    setTimeout(() => {
                        $('#username').removeClass('is-valid');
                        $('#username').addClass('is-invalid');
                        document.getElementById('invalid_user_feed').innerText = "Usuário inválido!";

                        $('#password').removeClass('is-valid');
                        $('#password').addClass('is-invalid');
                        document.getElementById('invalid_pass_feed').innerText = "Senha inválida!";

                        $('#btn_login').toggleClass('btn-primary btn-danger');
                        document.getElementById('btn_login').innerHTML = "Algo deu errado!";

                        setTimeout(() => {
                            $('#btn_login').toggleClass('btn-primary btn-danger');
                            document.getElementById('btn_login').innerHTML = "Fazer Login";
                        }, 1000)
                    }, 1000)
                } else {
                    document.getElementById('btn_login').innerHTML = "<div class='spinner-border'' role='status'></div >";
                    setTimeout(() => {
                        $('#div_user').addClass('hide');
                        $('#div_pass').addClass('hide');
                        $('#div_frgt').toggleClass('d-flex d-none');

                        $('#btn_login').toggleClass('btn-primary btn-success');
                        document.getElementById('btn_login').innerHTML = "Login bem sucedido!";

                        setTimeout(() => {
                            window.location.href = 'Views/Template';
                        }, 1000)
                    }, 1000)
                }
            })
            .catch(error => {
                console.error('There was a problem with the fetch operation:', error);
            });


    }

}