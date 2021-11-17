<?php

namespace App;

use Illuminate\Database\Eloquent\Model;

class InstructionModel extends Model
{
    protected $fillable = [
        'name', 'about', 'is_ok','filename',
    ];

    protected $table = 'instruction';
    public $timestamps = false;
}
