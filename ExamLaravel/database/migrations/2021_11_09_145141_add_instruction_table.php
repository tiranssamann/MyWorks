<?php

use Illuminate\Support\Facades\Schema;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Database\Migrations\Migration;

class AddInstructionTable extends Migration
{
    /**
     * Run the migrations.
     *
     * @return void
     */
    public function up()
    {
        Schema::table('instruction', function (Blueprint $table) {
            $table->integer('rate')->nullable();
            $table->integer('is_ok')->default(0);
          });
    }

    /**
     * Reverse the migrations.
     *
     * @return void
     */
    public function down()
    {
        Schema::table('instruction', function (Blueprint $table) {
            $table->dropColumn('rate');
            $table->dropColumn('is_ok');
          });
    }
}
