@extends('layouts.adm')

@section('content')
<div class="container">
    <form action="" method="post" enctype="multipart/form-data">
        <div class="form-row">
            <div class="form-group col-md-6">
                <label for="inputName4">Instruction</label>
                <input type="text" class="form-control" id="inputName4" placeholder="Instruction" name="instructionname">
            </div>
            <div class="form-group col-md-6">
                <label for="inputEmail4">Product</label>
                <input type="text" class="form-control" id="inputEmail4" placeholder="Product" name="Product">
            </div>
        </div>
        
        <div class="form-group">
            <div class="form-check">
               
                <input class="form-check-input" type="checkbox" id="gridCheck" name="ispublish">
                <label  for="gridCheck">
                    Is publish
                </label>
            </div>
        </div>
        <input type="file" name="file" id="replyfile">
        <input type="submit" value="Save Instruction">
        <input type="hidden" name="action" value="insert">
    </form>
</div>
@endsection