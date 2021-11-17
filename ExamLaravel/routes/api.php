<?php

use Illuminate\Http\Request;

/*
|--------------------------------------------------------------------------
| API Routes
|--------------------------------------------------------------------------
|
| Here is where you can register API routes for your application. These
| routes are loaded by the RouteServiceProvider within a group which
| is assigned the "api" middleware group. Enjoy building your API!
|
*/

Route::middleware('auth:api')->get('/user', function (Request $request) {
    return $request->user();
});

// POST http://laravel/api/students_rest
Route::post('students_rest/', 'StudentsController@rest_api');


// POST http://laravel/api/login
Route::post('login/', 'StudentsController@login_rest');
