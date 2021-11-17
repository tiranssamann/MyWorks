@extends('layouts.app')

@section('content')
<div class="container">
    <form  method="post" enctype="multipart/form-data">
        <div class="form-row">
            <div class="form-group col-md-6">
                <label for="inputName4">complaint</label>
                <input type="text" class="form-control" id="inputName4" placeholder="complaint" name="complaint">
            </div>
        </div>
        <input type="submit" value="Save Instruction">
        <input type="hidden" name="action" value="postdata">
    </form>
</div>
@endsection