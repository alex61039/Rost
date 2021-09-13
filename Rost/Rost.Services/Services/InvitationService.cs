using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Rost.Common.Enums;
using Rost.Repository;
using Rost.Repository.Domain;
using Rost.Services.Infrastructure;

namespace Rost.Services.Services
{
    public class InvitationService : IInvitationService
    {
        private readonly IUnitOfWork _unitOfWork;

        public InvitationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public async Task AddAsync(Invitation entity)
        {
            await _unitOfWork.Invitations.AddAsync(entity);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteAsync(Invitation entity)
        {
            _unitOfWork.Invitations.Remove(entity);
            await _unitOfWork.CommitAsync();
        }
        
        public async Task UpdateAsync(Invitation invitation)
        {
            await _unitOfWork.CommitAsync();
        }

        public async Task<Invitation> GetAsync(int id) => await _unitOfWork.Invitations.GetByIdAsync(id);

        public async Task<IEnumerable<Invitation>> ListAsync() => await _unitOfWork.Invitations.ListAsync();

        public async Task<IEnumerable<Invitation>> ListByUserId(string userId, bool isActive) => await _unitOfWork.Invitations.ListByUserId(userId, isActive);
    
        public async Task Accept(Invitation invitation, int[] children)
        {
            invitation.IsActive = false;
            invitation.Updated = DateTime.Now;
            _unitOfWork.Invitations.Update(invitation);

            foreach (var child in children)
            {
                var subscription = new Subscription()
                {
                    CommunityId = invitation.CommunityId,
                    ChildId = child,
                    Status = SubscriptionStatus.Accepted,
                    Updated = DateTime.Now
                };

                await _unitOfWork.Subscriptions.AddAsync(subscription);
            }

            await _unitOfWork.CommitAsync();
        }
    }
}