@extends('layouts.adm')

@section('content')
<div class="alert alert-dark" role="alert">
    <h3> Для изменения поля кликните по нему один раз</h3>
</div>

<table class="table table-hover table-dark">
    <thead>
        <tr>
            <th scope="col">#</th>
            <th scope="col">Name</th>
            <th scope="col">password</th>
            <th scope="col">email</th>
            <th scope="col">Is admin</th>
            <th scope="col">Is banned</th>
            <th scope="col">Delete</th>
        </tr>
    </thead>
    <tbody>
        @foreach ($users_array as $users)
        <tr>
            <th scope="row">{{ $users->id }}</th>
            <td>
                <div id="replybutton" class="btn4 like">
                    <span class="btn reply" onClick="$('#reply{{$users->name}}').toggle(); $('.reply{{$users->name}}').toggle();" style="color: white;">{{ $users->name }}</span>
                </div>
                <form action="" method="post">
                    <input type="text" name="username" id="reply{{$users->name}}" style="display:none;">
                    <input type="submit" class="reply{{$users->name}}" value="Edit" style="display:none;">
                    <input type="hidden" name="id" value="{{ $users->id }}">
                    <input type="hidden" name="action" value="edit_name">

                </form>
            </td>
            <td>
                <div id="replybutton" class="btn4 like">
                    <span class="btn reply" onClick="$('#replypassword{{ $users->id }}').toggle(); $('.replypassword{{ $users->id }}').toggle();" style="color: white;">{{ $users->password }}</span>
                </div>
                <form action="" method="post">
                    <input type="text" name="password" id="replypassword{{ $users->id }}" style="display:none;">
                    <input type="submit" value="Edit" class="replypassword{{ $users->id }}" style="display:none;">
                    <input type="hidden" name="id" value="{{ $users->id }}">
                    <input type="hidden" name="action" value="edit_password">

                </form>
            </td>
            <td>
                <div id="replybutton" class="btn4 like">
                    <span class="btn reply" onClick="$('#replyemail{{ $users->id }}').toggle(); $('.replyemail{{ $users->id }}').toggle();" style="color: white;">{{ $users->email }}</span>
                </div>
                <form action="" method="post">
                    <input type="text" name="email" id="replyemail{{ $users->id }}" style="display:none;">
                    <input type="submit" value="Edit" class="replyemail{{ $users->id }}" style="display:none;">
                    <input type="hidden" name="id" value="{{ $users->id }}">
                    <input type="hidden" name="action" value="edit_email">

                </form>
            </td>
            <td>
                <div id="replybutton" class="btn4 like">
                    <span class="btn reply" onClick="$('#reply{{$users->is_admin}}{{ $users->id }}').toggle(); $('.reply{{$users->is_admin}}{{ $users->id }}').toggle();" style="color: white;">{{ $users->is_admin }}</span>
                </div>
                <form action="" method="post">
                    <input type="text" name="is_admin" id="reply{{$users->is_admin}}{{ $users->id }}" style="display:none;">
                    <input type="submit" class="reply{{$users->is_admin}}{{ $users->id }}" value="Edit" style="display:none;">
                    <input type="hidden" name="id" value="{{ $users->id }}">
                    <input type="hidden" name="action" value="edit_is_admin">
                </form>
            </td>
            <td>
                <div id="replybutton" class="btn4 like">
                    <span class="btn reply" onClick="$('#reply{{$users->is_banned}}{{ $users->id }}').toggle(); $('.reply{{$users->is_banned}}{{ $users->id }}').toggle();" style="color: white;">{{ $users->is_banned }}</span>
                </div>
                <form action="" method="post">
                    <input type="text" name="is_banned" id="reply{{$users->is_banned}}{{ $users->id }}" style="display:none;">
                    <input type="submit" class="reply{{$users->is_banned}}{{ $users->id }}" value="Edit" style="display:none;">
                    <input type="hidden" name="id" value="{{ $users->id }}">
                    <input type="hidden" name="action" value="edit_is_banned">
                </form>
            </td>
            <td>
                <form action="" method="POST">
                    @csrf
                    <input type="submit" value="Delete">
                    <input type="hidden" name="id" value="{{ $users->id }}">
                    <input type="hidden" name="action" value="delete">
                </form>
            </td>
        </tr>
        @endforeach
    </tbody>
</table>

@endsection