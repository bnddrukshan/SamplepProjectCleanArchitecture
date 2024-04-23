﻿using SampleProject.Application.BaseFeature;
using SampleProject.Application.Mapper;
using SampleProject.Application.ViewModels;
using SampleProject.Domain.Interfaces;

namespace SampleProject.Application.Features.SampleModel.Queries.GetSampleModelById;

public class GetSampleModelByIdQueryHandler(IUnitOfWork unitOfWork, SampleModelMapper mapper) : IBaseCommandQueryHandler<GetSampleModelByIdQuery, SampleModelViewModel>
{
    public async Task<BaseResult<SampleModelViewModel>> Handle(GetSampleModelByIdQuery request, CancellationToken cancellationToken)
    {
        var result = new BaseResult<SampleModelViewModel>();

        var entity = await unitOfWork.SampleModelRepository.GetByIdAsync(request.Id, cancellationToken);
        if (entity is null)
        {
            result.AddErrorMessage(Resources.Messages.NotFound);
            return result;
        }

        var viewModel = mapper.ToViewModel(entity);

        result.AddValue(viewModel);
        result.AddSuccessMessage(Resources.Messages.SuccessAction);
        return result;
    }
}