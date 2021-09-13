using Microsoft.EntityFrameworkCore.Migrations;

namespace Rost.Repository.Migrations
{
    public partial class LinkSubscriptionsToChildren : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Community_Cities_CityId",
                table: "Community");

            migrationBuilder.DropForeignKey(
                name: "FK_Community_CommunityType_CommunityTypeId",
                table: "Community");

            migrationBuilder.DropForeignKey(
                name: "FK_Community_Districts_DistrictId",
                table: "Community");

            migrationBuilder.DropForeignKey(
                name: "FK_Community_Educations_EducationId",
                table: "Community");

            migrationBuilder.DropForeignKey(
                name: "FK_Community_MunicipalUnions_MunicipalUnionId",
                table: "Community");

            migrationBuilder.DropForeignKey(
                name: "FK_Community_AspNetUsers_UserId",
                table: "Community");

            migrationBuilder.DropForeignKey(
                name: "FK_Subscription_Community_CommunityId",
                table: "Subscription");

            migrationBuilder.DropForeignKey(
                name: "FK_Subscription_AspNetUsers_UserId",
                table: "Subscription");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Subscription",
                table: "Subscription");

            migrationBuilder.DropIndex(
                name: "IX_Subscription_UserId",
                table: "Subscription");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Community",
                table: "Community");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Subscription");

            migrationBuilder.RenameTable(
                name: "Subscription",
                newName: "Subscriptions");

            migrationBuilder.RenameTable(
                name: "Community",
                newName: "Communities");

            migrationBuilder.RenameIndex(
                name: "IX_Subscription_CommunityId",
                table: "Subscriptions",
                newName: "IX_Subscriptions_CommunityId");

            migrationBuilder.RenameIndex(
                name: "IX_Community_UserId",
                table: "Communities",
                newName: "IX_Communities_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Community_MunicipalUnionId",
                table: "Communities",
                newName: "IX_Communities_MunicipalUnionId");

            migrationBuilder.RenameIndex(
                name: "IX_Community_EducationId",
                table: "Communities",
                newName: "IX_Communities_EducationId");

            migrationBuilder.RenameIndex(
                name: "IX_Community_DistrictId",
                table: "Communities",
                newName: "IX_Communities_DistrictId");

            migrationBuilder.RenameIndex(
                name: "IX_Community_CommunityTypeId",
                table: "Communities",
                newName: "IX_Communities_CommunityTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Community_CityId",
                table: "Communities",
                newName: "IX_Communities_CityId");

            migrationBuilder.AddColumn<int>(
                name: "ChildId",
                table: "Subscriptions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Subscriptions",
                table: "Subscriptions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Communities",
                table: "Communities",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_ChildId",
                table: "Subscriptions",
                column: "ChildId");

            migrationBuilder.AddForeignKey(
                name: "FK_Communities_Cities_CityId",
                table: "Communities",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Communities_CommunityType_CommunityTypeId",
                table: "Communities",
                column: "CommunityTypeId",
                principalTable: "CommunityType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Communities_Districts_DistrictId",
                table: "Communities",
                column: "DistrictId",
                principalTable: "Districts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Communities_Educations_EducationId",
                table: "Communities",
                column: "EducationId",
                principalTable: "Educations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Communities_MunicipalUnions_MunicipalUnionId",
                table: "Communities",
                column: "MunicipalUnionId",
                principalTable: "MunicipalUnions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Communities_AspNetUsers_UserId",
                table: "Communities",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Subscriptions_Children_ChildId",
                table: "Subscriptions",
                column: "ChildId",
                principalTable: "Children",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Subscriptions_Communities_CommunityId",
                table: "Subscriptions",
                column: "CommunityId",
                principalTable: "Communities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Communities_Cities_CityId",
                table: "Communities");

            migrationBuilder.DropForeignKey(
                name: "FK_Communities_CommunityType_CommunityTypeId",
                table: "Communities");

            migrationBuilder.DropForeignKey(
                name: "FK_Communities_Districts_DistrictId",
                table: "Communities");

            migrationBuilder.DropForeignKey(
                name: "FK_Communities_Educations_EducationId",
                table: "Communities");

            migrationBuilder.DropForeignKey(
                name: "FK_Communities_MunicipalUnions_MunicipalUnionId",
                table: "Communities");

            migrationBuilder.DropForeignKey(
                name: "FK_Communities_AspNetUsers_UserId",
                table: "Communities");

            migrationBuilder.DropForeignKey(
                name: "FK_Subscriptions_Children_ChildId",
                table: "Subscriptions");

            migrationBuilder.DropForeignKey(
                name: "FK_Subscriptions_Communities_CommunityId",
                table: "Subscriptions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Subscriptions",
                table: "Subscriptions");

            migrationBuilder.DropIndex(
                name: "IX_Subscriptions_ChildId",
                table: "Subscriptions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Communities",
                table: "Communities");

            migrationBuilder.DropColumn(
                name: "ChildId",
                table: "Subscriptions");

            migrationBuilder.RenameTable(
                name: "Subscriptions",
                newName: "Subscription");

            migrationBuilder.RenameTable(
                name: "Communities",
                newName: "Community");

            migrationBuilder.RenameIndex(
                name: "IX_Subscriptions_CommunityId",
                table: "Subscription",
                newName: "IX_Subscription_CommunityId");

            migrationBuilder.RenameIndex(
                name: "IX_Communities_UserId",
                table: "Community",
                newName: "IX_Community_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Communities_MunicipalUnionId",
                table: "Community",
                newName: "IX_Community_MunicipalUnionId");

            migrationBuilder.RenameIndex(
                name: "IX_Communities_EducationId",
                table: "Community",
                newName: "IX_Community_EducationId");

            migrationBuilder.RenameIndex(
                name: "IX_Communities_DistrictId",
                table: "Community",
                newName: "IX_Community_DistrictId");

            migrationBuilder.RenameIndex(
                name: "IX_Communities_CommunityTypeId",
                table: "Community",
                newName: "IX_Community_CommunityTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Communities_CityId",
                table: "Community",
                newName: "IX_Community_CityId");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Subscription",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Subscription",
                table: "Subscription",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Community",
                table: "Community",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Subscription_UserId",
                table: "Subscription",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Community_Cities_CityId",
                table: "Community",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Community_CommunityType_CommunityTypeId",
                table: "Community",
                column: "CommunityTypeId",
                principalTable: "CommunityType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Community_Districts_DistrictId",
                table: "Community",
                column: "DistrictId",
                principalTable: "Districts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Community_Educations_EducationId",
                table: "Community",
                column: "EducationId",
                principalTable: "Educations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Community_MunicipalUnions_MunicipalUnionId",
                table: "Community",
                column: "MunicipalUnionId",
                principalTable: "MunicipalUnions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Community_AspNetUsers_UserId",
                table: "Community",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Subscription_Community_CommunityId",
                table: "Subscription",
                column: "CommunityId",
                principalTable: "Community",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Subscription_AspNetUsers_UserId",
                table: "Subscription",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
