<?php

/*
|--------------------------------------------------------------------------
| Web Routes
|--------------------------------------------------------------------------
|
| Here is where you can register web routes for your application. These
| routes are loaded by the RouteServiceProvider within a group which
| contains the "web" middleware group. Now create something great!
|
*/

// home page

use Illuminate\Http\Request;
use Illuminate\Support\Facades\Route;
use Illuminate\Support\Facades\DB;


Route::get('/', function(){
    $instruction_array = DB::select('SELECT * FROM instruction where is_ok = 1');
        return view('welcome', ['instruction_array' => $instruction_array]);
})->name('home');
Route::get('/', function (Request $request){
    $searchInst = $request->input('about');
    $instruction_array = DB::select("SELECT * FROM instruction where about like '%$searchInst%' and is_ok = 1");
    return view('welcome', ['instruction_array' => $instruction_array]);
})->name('searchinst');
Auth::routes();
Route::get('Admin/admin', 'AdminController@checkLogin')->name('admin');
Route::any('Admin/users-edit', 'AdminController@editUsers')->name('users-edit');
Route::any('Admin/instructions-edit', 'AdminController@editInstructions')->name('instructions-edit');
Route::any('Admin/instructions-add','AdminController@newInstruction')->name('instructions-add');
Route::any('Admin/users-add','AdminController@newUser')->name('users-add');
Route::any('Admin/complaints-show','AdminController@complaints')->name('complaints-show');
Route::any('Admin/instructions-accept','AdminController@acceptInst')->name('instructions-accept');
Route::any('pdfread', 'PDFController@viewPDG')->name('pdfread');
Route::any('pdfreadView', 'PDFController@viewPDGp')->name('pdfreadView');
Route::any('pdfread/download', 'PDFController@downloadpdf')->name('pdfread/download');
Route::any('complaintUs', 'UserController@complaintsUser')->name('complaintUs');
Route::any('add-instruction', 'UserController@addinstr')->name('add-instruction');
Route::any('vieworder', 'UserController@vieworder')->name('vieworder');