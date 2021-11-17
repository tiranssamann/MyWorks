@extends('layouts.adm')

@section('content')
<div class="container">
    <form action="" method="post">
        <div class="form-row">
            <div class="form-group col-md-6">
                <label for="inputName4">Name</label>
                <input type="text" class="form-control" id="inputName4" placeholder="Name" name="username">
            </div>
            <div class="form-group col-md-6">
                <label for="inputEmail4">Email</label>
                <input type="email" class="form-control" id="inputEmail4" placeholder="Email" name="email">
            </div>
        </div>
        <div class="form-group">
            <label for="inputPassword">Password</label>
            <input type="password" class="form-control" id="inputPassword" placeholder="Password" name="password">
        </div>
        <div class="form-group">
            <div class="form-check">
               
                <input class="form-check-input" type="checkbox" id="gridCheck" name="isadmin">
                <label  for="gridCheck">
                    Is admin
                </label>
            </div>
        </div>
        <input type="submit" value="Save User">
        <input type="hidden" name="action" value="insert">
    </form>
</div>
@endsection